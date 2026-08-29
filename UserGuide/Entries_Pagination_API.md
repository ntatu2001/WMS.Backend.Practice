# API: Tra cứu & Phân trang Receipt/Issue Entries

> Tài liệu mô tả 4 API tra cứu lô nhập/xuất kho: `GetReceiptEntriesNotPendingByDate`, `GetIssueEntriesNotPendingByDate` (tìm theo khoảng ngày) và `GetReceiptEntriesByLotNumber`, `GetIssueEntriesByLotNumber` (tìm gần đúng theo **mã lô hàng** và/hoặc **tên sản phẩm**) — cả 4 đều dùng chung cơ chế **phân trang tại Backend** (`pageNumber`, `pageSize`), filter theo **tên kho** (`warehouseName`), **sắp xếp theo tiến độ**, và **luôn loại bỏ các lô đang ở trạng thái `Pending` (Chờ xử lý)**, thay cho việc trước đây FE phải tải toàn bộ danh sách rồi tự phân trang ở client.
>
> Xem quy ước chung (base URL, auth, error envelope...) tại [`API_Guide_For_Frontend.md`](./API_Guide_For_Frontend.md).

---

## 1. Vì sao thay đổi

Trang **"Quản lý nhập kho" / "Quản lý xuất kho"** trước đây gọi 2 API này mà không có tham số phân trang — Backend trả về **toàn bộ** danh sách lô nhập/xuất, FE mới cắt trang ở client. Với dữ liệu lớn, cách này gây delay tải trang. Nay Backend đã hỗ trợ `Skip`/`Take` ngay ở tầng truy vấn database, chỉ query và xử lý đúng số dòng cần cho 1 trang.

**Cập nhật mới nhất:** 2 API `GetReceiptEntriesByDate`/`GetIssueEntriesByDate` đã được **đổi tên** thành `GetReceiptEntriesNotPendingByDate`/`GetIssueEntriesNotPendingByDate` để phản ánh đúng hành vi — các API này (và cả 2 API `...ByLotNumber` vì dùng chung handler) giờ **luôn loại bỏ** các lô đang ở trạng thái `Pending` (Chờ xử lý) khỏi kết quả trả về, không cần FE tự lọc. FE cần cập nhật lại URL đang gọi sang tên endpoint mới — route cũ không còn tồn tại.

---

## 2. Endpoint

| Method | URL | Role yêu cầu | Tiêu chí tìm chính |
|---|---|---|---|
| GET | `/WarehouseAPI/InventoryReceiptEntry/GetReceiptEntriesNotPendingByDate` | `Manager`, `Admin` | Khoảng ngày (`fromDate`/`toDate`) |
| GET | `/WarehouseAPI/InventoryIssueEntry/GetIssueEntriesNotPendingByDate` | `Manager`, `Admin` | Khoảng ngày (`fromDate`/`toDate`) |
| GET | `/WarehouseAPI/InventoryReceiptEntry/GetReceiptEntriesByLotNumber` | `Manager`, `Admin` | Mã lô hàng (`lotNumber`) và/hoặc tên sản phẩm (`materialName`), tìm gần đúng |
| GET | `/WarehouseAPI/InventoryIssueEntry/GetIssueEntriesByLotNumber` | `Manager`, `Admin` | Mã lô hàng (`lotNumber`) và/hoặc tên sản phẩm (`materialName`), tìm gần đúng |

Yêu cầu header `Authorization: Bearer <accessToken>`.

2 API `...ByLotNumber` dùng chung handler/response với 2 API `...ByDate` — chỉ khác tiêu chí tìm chính là `lotNumber`/`materialName` thay vì khoảng ngày (`fromDate`/`toDate` không áp dụng ở 2 API này, xem mục 4.1). Toàn bộ phần **thứ tự sắp xếp (mục 3)**, **response (mục 5)**, và **phân trang** đều áp dụng giống hệt nhau cho cả 4 API.

## 3. Loại bỏ trạng thái `Pending` & Thứ tự sắp xếp theo tiến độ

⚠️ **Cả 4 API luôn loại bỏ các lô có `LotStatus = Pending` (Chờ xử lý)** khỏi kết quả trả về — không có tham số nào để bật lại việc hiển thị các lô này. Nếu FE cần xem cả lô Pending (ví dụ màn hình duyệt phiếu chờ xử lý), phải dùng API khác (báo lại nếu cần bổ sung).

Sau khi loại Pending, kết quả còn lại (cả khi phân trang lẫn khi trả full list) được **Backend sắp xếp sẵn theo tiến độ lô hàng** (`LotStatus` của `receiptLot`/`issueLot`), FE **không cần** tự sort lại:

| Thứ tự | `LotStatus` (giá trị enum) | Nhãn hiển thị tiếng Việt |
|---|---|---|
| 1 | `InProgress` | Đang thực hiện |
| 2 | `Done` | Hoàn thành |
| 3 | `HoldOn` | Tạm hoãn |
| 4 | `IsBlocked` | Bị chặn |
| 5 | `Cancelled` | Đã huỷ |

Trong cùng 1 nhóm tiến độ, các lô được sắp xếp phụ theo `receiptDate`/`issueDate` **giảm dần** (mới nhất trước).

⚠️ Việc lọc `Pending` và sort theo tiến độ đều được áp dụng **trước** khi cắt trang (`Skip`/`Take`) — nghĩa là `totalItems` cũng đã loại trừ các lô Pending, và trang 1 sẽ luôn ưu tiên hiển thị các lô "Đang thực hiện" trước, đúng với mục đích UI muốn người dùng thấy ngay các lô cần xử lý gấp lên đầu danh sách.

## 4. Query Parameters

### 4.1. `GetReceiptEntriesNotPendingByDate` / `GetIssueEntriesNotPendingByDate`

Tất cả tham số đều **optional** — API tương thích ngược hoàn toàn với cách gọi cũ.

| Param | Kiểu | Bắt buộc | Mô tả |
|---|---|---|---|
| `fromDate` | `DateTime` (ISO 8601) | Không | Lọc từ ngày (theo `ReceiptDate`/`IssueDate`, so sánh theo `.Date`, không tính giờ) |
| `toDate` | `DateTime` (ISO 8601) | Không | Lọc đến ngày |
| `warehouseName` | `string` | Không | Lọc theo **tên loại kho** (khớp chính xác), ví dụ `"Kho Nguyên vật liệu"`, `"Kho Thành phẩm"`. Vì nhiều kho có thể dùng chung 1 tên (VD `NVL01` và `NVL02` cùng là `"Kho Nguyên vật liệu"`), truyền tên sẽ trả về lô hàng của **tất cả** các kho cùng loại đó, không chỉ 1 kho cụ thể. |
| `pageNumber` | `int` | Không | Số trang, bắt đầu từ `1` |
| `pageSize` | `int` | Không | Số bản ghi mỗi trang |

### 4.2. `GetReceiptEntriesByLotNumber` / `GetIssueEntriesByLotNumber`

| Param | Kiểu | Bắt buộc | Mô tả |
|---|---|---|---|
| `lotNumber` | `string` | Không | Mã lô hàng cần tìm — khớp **gần đúng** (substring, không phân biệt hoa/thường, tương đương SQL `LIKE '%...%'`). Với Receipt là `LotNumber` của lô nhập; với Issue là `MaterialLotId` gắn trên `issueLot` (chính là `lotNumber` trả về trong response, xem mục 5.2). |
| `materialName` | `string` | Không | Tên sản phẩm/vật tư cần tìm — khớp **gần đúng** (substring, không phân biệt hoa/thường), tương tự `lotNumber`. |
| `warehouseName` | `string` | Không | Giống mục 4.1 — lọc thêm theo loại kho nếu cần |
| `pageNumber` | `int` | Không | Số trang, bắt đầu từ `1` |
| `pageSize` | `int` | Không | Số bản ghi mỗi trang |

- `lotNumber` và `materialName` đều optional và có thể dùng **độc lập hoặc kết hợp** — nếu truyền cả hai, kết quả phải khớp gần đúng **cả hai** điều kiện (AND, không phải OR).
- Không truyền cả `lotNumber` lẫn `materialName` → API trả về toàn bộ (chỉ áp dụng `warehouseName`/phân trang nếu có) — tương đương gọi `GetReceiptEntriesNotPendingByDate`/`GetIssueEntriesNotPendingByDate` không kèm `fromDate`/`toDate`.
- Lưu ý: 2 API này **không có** `fromDate`/`toDate`. Nếu cần tìm theo ngày **và** theo mã lô/tên sản phẩm cùng lúc cho 1 màn hình, báo lại để gộp `lotNumber`/`materialName` như filter phụ vào `GetReceiptEntriesNotPendingByDate`/`GetIssueEntriesNotPendingByDate` thay vì gọi 2 API riêng.

⚠️ **Lưu ý chung về hành vi phân trang (áp dụng cho cả 4 API):**
- Chỉ khi **cả hai** `pageNumber` và `pageSize` đều được truyền thì Backend mới cắt trang.
- Nếu thiếu 1 trong 2 (hoặc không truyền gì cả) → Backend trả về **toàn bộ** kết quả đã lọc (như hành vi cũ trước khi có phân trang).
- ⚠️ Tên tham số ở đây là `pageNumber`/**`pageSize`**, khác với 2 API phân trang khác đã có sẵn trong hệ thống (`GetAllLocations`, `GetAllMaterials`) dùng `pageNumber`/**`itemsPerPage`** — chú ý không nhầm lẫn khi tích hợp.

## 5. Response

Response đổi từ trả thẳng mảng `[...]` sang dạng bọc `QueryResult<T>` — **đây là thay đổi phá vỡ tương thích (breaking change)**, FE bắt buộc phải cập nhật lại cách đọc response cho cả 4 API (`GetReceiptEntriesByLotNumber`/`GetIssueEntriesByLotNumber` dùng chung `InventoryReceiptEntryDTO`/`InventoryIssueEntryDTO` giống mục 5.1/5.2 bên dưới).

```json
{
  "results": [ /* danh sách lô hàng của trang hiện tại */ ],
  "totalItems": 0
}
```

- `results`: mảng các item (xem cấu trúc bên dưới).
- `totalItems`: tổng số bản ghi khớp filter (không phụ thuộc `pageNumber`/`pageSize`) — dùng để FE tự tính `totalPages = Math.ceil(totalItems / pageSize)`. Response **không** trả kèm `pageNumber`/`pageSize`/`totalPages`.

### 5.1. Cấu trúc item — `GetReceiptEntriesNotPendingByDate` (`InventoryReceiptEntryDTO`)

```ts
{
  inventoryReceiptEntryId: string
  purchaseOrderNumber: string
  materialName: string
  materialId: string
  note: string
  inventoryReceiptId: string
  lotNumber: string
  warehouseName: string
  personName: string        // tên nhân viên tạo phiếu nhập
  unit: string
  receiptDate: string        // ISO 8601
  receiptLot: {
    receiptLotId: string
    importedQuantity: number
    receiptLotStatus: string   // enum dạng string, VD "InProgress" | "Done" (không bao giờ là "Pending", xem mục 3)
    inventoryReceiptEntryId: string
    materialId: string
    materialName: string
    storageLevel: string
    warehouseId: string
    warehouseName: string
    receiptSublots: [ /* danh sách lô phụ đã nhập, xem ReceiptSubLotDTO */ ]
  }
}
```

### 5.2. Cấu trúc item — `GetIssueEntriesNotPendingByDate` (`InventoryIssueEntryDTO`)

```ts
{
  inventoryIssueEntryId: string
  purchaseOrderNumber: string
  requestedQuantity: number
  note: string
  materialName: string
  materialId: string
  unit: string
  inventoryIssueId: string
  warehouseId: string
  warehouseName: string
  personId: string
  personName: string          // tên nhân viên tạo phiếu xuất
  lotNumber: string
  issueDate: string            // ISO 8601
  issueLot: {
    issueLotId: string
    requestedQuantity: number
    issueLotStatus: string     // enum dạng string
    materialLotId: string
    inventoryIssueEntryId: string
    issueSublots: [ /* danh sách lô phụ đã xuất, gồm materialSublot, locationId (snapshot vị trí lúc xuất)... */ ]
  }
}
```

## 6. Ví dụ gọi API

**Lấy trang 1, 20 dòng/trang, lọc theo khoảng ngày và loại kho:**
```
GET /WarehouseAPI/InventoryReceiptEntry/GetReceiptEntriesNotPendingByDate?fromDate=2026-08-01&toDate=2026-08-22&warehouseName=Kho%20Nguy%C3%AAn%20v%E1%BA%ADt%20li%E1%BB%87u&pageNumber=1&pageSize=20
```

**Không phân trang, chỉ lọc theo ngày (hành vi tương thích ngược):**
```
GET /WarehouseAPI/InventoryIssueEntry/GetIssueEntriesNotPendingByDate?fromDate=2026-08-01&toDate=2026-08-22
```

**Tìm lô nhập kho theo mã lô hàng gần đúng, có phân trang:**
```
GET /WarehouseAPI/InventoryReceiptEntry/GetReceiptEntriesByLotNumber?lotNumber=RL_10&pageNumber=1&pageSize=20
```

**Tìm lô xuất kho theo tên sản phẩm gần đúng, kèm lọc theo loại kho:**
```
GET /WarehouseAPI/InventoryIssueEntry/GetIssueEntriesByLotNumber?materialName=Bao%20b%C3%AC&warehouseName=Kho%20Th%C3%A0nh%20ph%E1%BA%A9m
```

**Kết hợp cả mã lô và tên sản phẩm (AND):**
```
GET /WarehouseAPI/InventoryReceiptEntry/GetReceiptEntriesByLotNumber?lotNumber=RL&materialName=Th%C3%A9p&pageNumber=1&pageSize=20
```

## 7. Gợi ý tích hợp UI phân trang

1. FE gửi `pageNumber` (bắt đầu từ 1) và `pageSize` cố định (VD 20/50) theo lựa chọn người dùng.
2. Đọc `totalItems` từ response, tính `totalPages = Math.ceil(totalItems / pageSize)`.
3. Khi người dùng đổi trang hoặc đổi bộ lọc (`fromDate`/`toDate`/`warehouseName`) → gọi lại API với `pageNumber` mới, giữ nguyên `pageSize`; nên reset về `pageNumber=1` mỗi khi bộ lọc thay đổi.
4. Vì `warehouseName` lọc theo **loại kho** (có thể gộp nhiều kho vật lý), nếu UI cần cho người dùng chọn 1 kho cụ thể (VD chỉ `NVL02`, không phải cả `NVL01`+`NVL02`), dùng API `GetLocationsByWarehouseId`/`GetAllWarehouseNameId` để lấy `WarehouseId` cụ thể và lọc thêm ở phía client, vì API này hiện chưa hỗ trợ lọc theo `warehouseId` chính xác.
5. Vì kết quả đã được sắp theo tiến độ sẵn (mục 3), UI có thể hiển thị badge/màu tương ứng theo nhóm trạng thái (VD: xanh dương = Đang thực hiện, xanh lá = Hoàn thành, cam = Tạm hoãn, đỏ = Bị chặn, xám = Đã huỷ) mà **không cần** gọi thêm API hay sort lại ở client. Lô "Chờ xử lý" (Pending) sẽ không bao giờ xuất hiện trong 4 API này (xem mục 3), nên không cần chuẩn bị badge cho trạng thái đó.
6. `lotNumber`/`materialName` ở `GetReceiptEntriesByLotNumber`/`GetIssueEntriesByLotNumber` khớp **gần đúng**, nên ô tìm kiếm trên UI có thể là ô nhập text tự do (không bắt buộc chọn từ danh sách có sẵn) — phù hợp cho thanh search-as-you-type. Nên debounce request (VD 300–500ms sau khi người dùng ngừng gõ) trước khi gọi API để tránh gọi quá nhiều lần, và luôn reset `pageNumber=1` mỗi khi từ khoá tìm kiếm thay đổi.
