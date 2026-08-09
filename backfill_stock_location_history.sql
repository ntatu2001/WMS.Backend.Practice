/*
  Backfill script for StockLocationHistories.

  Purpose:
    The StockLocationHistories table was introduced after a lot of MaterialSubLot
    rows already existed in the database (seeded directly, not through the
    Create/Update MaterialLot APIs). Those existing sublots have no history rows,
    so GetStockLocationHistoriesByLocationId returns empty for them.

    This script inserts one "Inbound" history row per existing MaterialSubLot,
    using its current LocationId and ExistingQuantity as a best-effort snapshot.
    It does NOT attempt to reconstruct any earlier moves/exports the sublot may
    have gone through before this table existed - it only guarantees each sublot
    has at least one history entry reflecting its current state.

  Idempotency:
    Safe to run multiple times - it skips any MaterialSubLot that already has an
    Inbound history row.
*/

INSERT INTO [StockLocationHistories]
    ([StockLocationHistoryId], [MaterialSubLotId], [LotNumber], [LocationId], [Quantity], [MovementType], [EventDate], [Id])
SELECT
    LOWER(CAST(NEWID() AS NVARCHAR(36))) AS StockLocationHistoryId,
    msl.[MaterialSubLotId],
    msl.[LotNumber],
    msl.[LocationId],
    msl.[ExistingQuantity],
    N'Inbound' AS MovementType,
    GETUTCDATE() AS EventDate,
    0 AS Id
FROM [MaterialSubLots] msl
WHERE NOT EXISTS (
    SELECT 1
    FROM [StockLocationHistories] slh
    WHERE slh.[MaterialSubLotId] = msl.[MaterialSubLotId]
      AND slh.[MovementType] = N'Inbound'
);

SELECT @@ROWCOUNT AS RowsInserted;
