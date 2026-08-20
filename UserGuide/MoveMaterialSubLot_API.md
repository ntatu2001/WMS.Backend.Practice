# API: Di chuyển vị trí Lô phụ (MoveMaterialSubLot)

> Tài liệu mô tả chi tiết API mới `MoveMaterialSubLot`, phục vụ tính năng kéo/thả lô phụ (MaterialSubLot) từ vị trí này sang vị trí khác trên giao diện **Sơ đồ kho**.
>
> Xem quy ước chung (base URL, auth, error envelope...) tại [`API_Guide_For_Frontend.md`](./API_Guide_For_Frontend.md).

---

## 1. Endpoint

| Method | URL |
|---|---|
| PUT | `/WarehouseAPI/MaterialSubLot/MoveMaterialSubLot` |

Yêu cầu header `Authorization: Bearer <accessToken>` như mọi endpoint khác. **Không giới hạn theo Role** (không có `[Authorize(Roles = ...)]` riêng trên endpoint này — mọi user đã đăng nhập hợp lệ đều gọi được).

## 2. Request Body

```ts
MoveMaterialSubLotCommand {
  materialSubLotId: string   // Id của lô phụ cần di chuyển, ví dụ "SL81"
  toLocationId: string       // Id vị trí đích, ví dụ "TP01.1.2.2.1"
}
```

Ví dụ:
```json
{
  "materialSubLotId": "SL81",
  "toLocationId": "TP01.1.2.2.1"
}
```

## 3. Response

Trả `bool` (giống các API update khác trong hệ thống — không có DTO chi tiết trả về).

```json
true
```

- `true`: di chuyển thành công (hoặc vị trí đích **trùng** vị trí hiện tại — coi là no-op, không báo lỗi, không ghi log gì thêm).
- Lỗi: xem mục 4.

> Vì response chỉ là `bool`, sau khi gọi thành công FE nên tự gọi lại API lấy dữ liệu vị trí (`GetLocationsByWarehouseId`, `GetInforByLocationId`) để cập nhật lại UI Sơ đồ kho (tỷ lệ lấp đầy, danh sách lô tại từng ô) thay vì tự suy ra từ response.

## 4. Các trường hợp lỗi (theo đúng thứ tự kiểm tra ở backend)

Tất cả đều trả **HTTP 400**, cấu trúc `ErrorResponse` chuẩn (`{ code, message, detail }`) như mô tả ở tài liệu chung.

| # | Tình huống | `code` | `detail` | Gợi ý hiển thị cho user |
|---|---|---|---|---|
| 1 | `materialSubLotId` không tồn tại | `NotFound.MaterialSubLot` | `{ entityType: "MaterialSubLot", entityId }` | "Lô phụ không tồn tại" |
| 2 | `toLocationId` không tồn tại | `NotFound.Location` | `{ entityType: "Location", entityId }` | "Vị trí đích không tồn tại" |
| 3 | Vị trí đích thuộc **kho khác** với vị trí hiện tại của lô phụ | `Unexpected` | message gốc: *"Cannot move MaterialSubLot {id} to Location {id}: source and destination are in different warehouses"* | "Không thể di chuyển sang kho khác" |
| 4 | Lô hàng (theo `LotNumber` của lô phụ) đang có **Kiểm kê (StockTake) ở trạng thái Pending** | `Unexpected` | message gốc: *"...has a pending StockTake. Please complete or cancel it before moving this sublot."* | "Lô hàng đang có đợt kiểm kê chưa hoàn tất, không thể di chuyển" |
| 5 | Vị trí đích thiếu thuộc tính `Length`/`Width`/`Height` nên không tính được sức chứa | `Unexpected` | message gốc | "Vị trí đích chưa cấu hình kích thước, không thể kiểm tra sức chứa" |
| 6 | Material của lô phụ thiếu thuộc tính `PacketSize`/`VolumePacket` nên không tính được thể tích | `Unexpected` | message gốc | "Không thể tính thể tích lô hàng này" |
| 7 | **Tỷ lệ lấp đầy vị trí đích sau khi thêm ≥ 100%** | `LocationCapacityExceeded` | xem cấu trúc bên dưới | Hiển thị số liệu chi tiết cho user (xem mục 5) |

⚠️ Các mục #3–#6 hiện dùng `Exception` chung (`code = "Unexpected"`), giống cách nhiều API khác trong hệ thống xử lý lỗi nghiệp vụ chưa có exception riêng — FE nên **match theo nội dung `message`** (hoặc theo tiền tố câu) nếu cần phân biệt case cụ thể để hiển thị UI khác nhau, vì `code` của các case này giống nhau.

## 5. Lỗi vượt sức chứa — `LocationCapacityExceeded`

Đây là lỗi nghiệp vụ chính mà FE cần xử lý kỹ nhất (ứng với yêu cầu "báo lỗi chi tiết" khi kéo-thả vượt sức chứa).

```json
{
  "code": "LocationCapacityExceeded",
  "message": "Location TP01.1.2.2.1 would reach 112.50% storage rate after adding this sublot, exceeding 100% capacity.",
  "detail": {
    "locationId": "TP01.1.2.2.1",
    "maxVolume": 3.234,
    "currentUsedVolume": 2.1,
    "incomingVolume": 1.5,
    "resultingRate": 112.5
  }
}
```

| Field trong `detail` | Ý nghĩa |
|---|---|
| `locationId` | Vị trí đích đang được kiểm tra |
| `maxVolume` | Sức chứa tối đa của vị trí (m³, tính từ `Length × Width × Height`) |
| `currentUsedVolume` | Thể tích đã dùng ở vị trí đích **trước khi** thêm lô phụ (không tính lô đang di chuyển nếu nó tình cờ đã ở đó) |
| `incomingVolume` | Thể tích của lô phụ đang di chuyển |
| `resultingRate` | Tỷ lệ lấp đầy dự kiến sau khi thêm (%), = `(currentUsedVolume + incomingVolume) / maxVolume × 100` |

Gợi ý UI: hiển thị dạng "Vị trí {locationId} hiện dùng {currentUsedVolume}/{maxVolume} m³, thêm lô này ({incomingVolume} m³) sẽ thành {resultingRate}% — vượt quá sức chứa cho phép (100%)."

## 6. Điều xảy ra ở backend khi di chuyển thành công

FE không cần tự xử lý các bước này, nhưng nên biết để giải thích hành vi hệ thống nếu người dùng thắc mắc:

1. `MaterialSubLot.LocationId` được cập nhật sang `toLocationId` — **luôn di chuyển toàn bộ số lượng hiện có**, không hỗ trợ tách một phần số lượng sang vị trí khác.
2. Ghi 2 bản ghi vào bảng lịch sử vị trí (`StockLocationHistory`): 1 dòng `Outbound` tại vị trí cũ, 1 dòng `Inbound` tại vị trí mới — dùng để đối chiếu qua API `GET /WarehouseAPI/Location/GetStockLocationHistoriesByLocationId`.
3. Các phiếu **xuất kho đã tạo trước đó** cho lô phụ này (`IssueSubLot`) sẽ **không** bị ảnh hưởng — chúng lưu snapshot vị trí tại đúng thời điểm xuất kho, không đổi theo vị trí mới.

## 7. Ràng buộc nghiệp vụ cần biết khi thiết kế UI Sơ đồ kho

- **Chỉ di chuyển trong cùng 1 kho** — UI không nên cho phép kéo-thả lô phụ sang ô thuộc kho khác (hoặc nếu cho kéo thì phải xử lý lỗi #3 ở mục 4).
- **Luôn di chuyển toàn bộ lô phụ** — không có khái niệm "di chuyển một phần số lượng" ở phiên bản này.
- **Không thể di chuyển lô đang có Kiểm kê Pending** — nếu UI Sơ đồ kho và UI Kiểm kê dùng chung, cân nhắc disable/ẩn thao tác kéo-thả cho các lô đang trong đợt kiểm kê dở dang, hoặc hiển thị rõ lý do khi lỗi #4 xảy ra.
- Nên **validate sơ bộ ở FE trước khi gọi API** (ví dụ ước lượng % lấp đầy hiển thị sẵn trên từng ô vị trí qua API `GetLocationsByWarehouseId`/`GetInforByLocationId`) để giảm trải nghiệm "kéo xong mới báo lỗi", nhưng **vẫn phải xử lý lỗi `LocationCapacityExceeded` từ server** làm nguồn sự thật cuối cùng (tránh race condition khi nhiều người dùng thao tác đồng thời).
