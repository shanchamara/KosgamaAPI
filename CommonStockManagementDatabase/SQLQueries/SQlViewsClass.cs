using Microsoft.EntityFrameworkCore.Migrations;

namespace CommonStockManagementDatabase.SQLQueries
{
    public class SQlViewsClass
    {
        public string VWAllActiveItemList = @"CREATE VIEW VWAllActiveItemList AS 
            SELECT
                    item.ItemName,
                    item.LastPurchasePrice,
                    item.ItemDescription,
                    item.FkCategoryId,
                    item.SellingPrice,
                    item.ItemCode,
                    Category.Name AS CategoryName,
                    brand.Name AS BrandName,
                    Units.Name AS UnitName,
                    item.Id,
                    item.Edit_By,
                    item.Edit_Date,
                    item.Delete_By,
                    item.Delete_Date,
                    item.IsDelete,
                    item.MaxLevel,
                    item.MinLevel,
                    item.ReorderLevel,
                    item.FkUnitId,
                    item.UnitSize,
                    item.FkBrandId,
                    item.ImageUrl
                    
                FROM
                    TblStock_Main AS item
                    INNER JOIN TblItemCategories AS Category ON item.FkCategoryId = Category.Id
                    INNER JOIN TblItemBrandNames AS brand ON item.FkBrandId = brand.Id
                    INNER JOIN TblItemUnits AS Units ON item.FkUnitId = Units.Id
                    
                WHERE
                    item.IsDelete = FALSE";

        public string VWListClient = @"CREATE VIEW VWListClient AS SELECT * FROM TblClients";

        public string VwBestCustomers = @"CREATE VIEW VwBestCustomers AS 
                            SELECT 
                posHead.Id,
                posHead.Date AS InvoiceDate,
                posHead.`Type`,
                posHead.Description,
                posHead.RefInv,
                posHead.Created,
                posHead.FKClientId,
                SUM(posHead.Total) AS Total,  
                SUM(posHead.Discount) AS Discount, 
                SUM(posHead.Gross) AS Gross,  
                posHead.IsDelete,
                MAX(posHead.Edit_By) AS Edit_By, 
                MAX(posHead.Edit_Date) AS Edit_Date,
                MAX(posHead.Delete_Date) AS Delete_Date,
                MAX(posHead.Delete_By) AS Delete_By,
                u.FirstName AS UserName,
                CONCAT(c.FirstName, ' ', c.LastName) AS Customer,
                location.companyName AS locationName,
                location.ID AS LocationId
            FROM TblPOSHead AS posHead
            INNER JOIN TblUsers AS u ON posHead.Created = u.id
            INNER JOIN TblClients AS c ON posHead.FKClientId = c.ID
            INNER JOIN TblCompanyDetails AS location ON posHead.FKLocationId = location.ID
            GROUP BY 
                posHead.Id,  
                posHead.Date, 
                posHead.`Type`,
                posHead.Description,
                posHead.RefInv,
                posHead.Created,
                posHead.FKClientId,
                posHead.IsDelete,
                u.FirstName,
                c.FirstName,
                c.LastName,
                location.companyName,
                location.ID
            ORDER BY Gross DESC
            LIMIT 10;

        ";


        public string VwListSupplier = @"CREATE VIEW VwListSupplier AS SELECT * FROM TblSupplier";


        public string VwListItemCategory = @"CREATE VIEW VwListItemCategory AS SELECT * FROM TblItemCategories";

        public string VwListItemUnit = @"CREATE VIEW VwListItemUnit AS SELECT * FROM TblItemUnits";

        public string VwListItemBrand = @"CREATE VIEW VwListItemBrand AS SELECT * FROM TblItemBrandNames";

        public string VwListGINHead = @"CREATE VIEW VwListGINHead AS SELECT * FROM TblGINHead";

        public string VwListGRNHeads = @"CREATE VIEW VwListGRNHeads AS 
                SELECT
                    GrnHead.Id,
                    GrnHead.Description,
                    GrnHead.Pono,
                    GrnHead.Date as InvoiceDate,
                    GrnHead.`Type`,
                    GrnHead.GRNType,
                    GrnHead.RefInv,
                    GrnHead.Created,
                    GrnHead.FKSupplier_ID,
                    GrnHead.Total,
                    GrnHead.Discount,
                    GrnHead.Gross,
                    GrnHead.IsDelete,
                    GrnHead.Edit_By,
                    GrnHead.Edit_Date,
                    GrnHead.Delete_By,
                    GrnHead.Delete_Date,
                    u.FirstName,
                    s.Company AS Supplier,
                    location.CompanyName AS locationName,
                    location.ID AS LocationId
                FROM
                    TblGRNHead AS GrnHead
                    INNER JOIN TblUsers AS u ON GrnHead.Created = u.id
                    INNER JOIN TblSupplier AS s ON GrnHead.FKSupplier_ID = s.id
                    JOIN TblCompanyDetails AS location ON  GrnHead.FKLocationId= location.ID";

        public string VWAllActiveItemReorderLevelList = @"CREATE VIEW VWAllActiveItemReorderLevelList AS 
                                    SELECT
                     ItemDetails.ReorderLevel,
                     IFNULL(
                         (tbl.PurchaseQty + tbl.CustomerRetrunQty) -(tbl.RetrunQty + tbl.SaleQty + tbl.IssueQty),
                         0
                     ) AS BalanceQty,
                     ItemDetails.MinLevel,
                     ItemDetails.MaxLevel,
                     ItemDetails.ItemName,
                     ItemDetails.ItemDescription,
                     ItemDetails.LastPurchasePrice,
                     ItemDetails.SellingPrice,
                     ItemDetails.ItemCode,
                     ItemDetails.CategoryName,
                     ItemDetails.FkCategoryId,
                     ItemDetails.FkBrandId,
                     ItemDetails.ImageUrl,
                     ItemDetails.BrandName,
                     ItemDetails.UnitName,
                     ItemDetails.UnitSize,
                     ItemDetails.FkUnitId,
                     ItemDetails.Id,
                     ItemDetails.IsDelete,
                     LocationDetails.LocationName,
                     LocationDetails.ID AS LocationID
                 FROM
                     (
                         (
                             SELECT
                                 item.ReorderLevel,
                                 item.MinLevel,
                                 item.MaxLevel,
                                 item.UnitSize,
                                 item.FkUnitId,
                                 item.ItemName,
                                 item.ItemDescription,
                                 item.IsDelete,
                                 item.LastPurchasePrice,
                                 item.SellingPrice,
                                 item.ItemCode,
                                 item.FkCategoryId,
                                 item.ImageUrl,
                                 Category.Name AS CategoryName,
                                 item.FkBrandId,
                                 brand.Name AS BrandName,
                                 Units.Name AS UnitName,
                                 item.Id
                             FROM
                                 TblStock_Main AS item
                                 INNER JOIN TblItemCategories AS Category ON item.FkCategoryId = Category.Id
                                 INNER JOIN TblItemBrandNames AS brand ON item.FkBrandId = brand.Id
                                 INNER JOIN TblItemUnits AS Units ON item.FkUnitId = Units.Id
                             WHERE
                                 item.IsDelete = FALSE
                         ) AS ItemDetails
                          JOIN (
                             SELECT
                                 location.ID,
                                 location.companyName AS LocationName
                             FROM
                                 TblCompanyDetails AS location
                         ) AS LocationDetails
        
                         LEFT OUTER JOIN (
                             SELECT
                                 IFNULL(GrnHead.PurchaseQty, 0) AS PurchaseQty,
                                 IFNULL(PosHead.SaleQty, 0) AS SaleQty,
                                 IFNULL(SRNHead.RetrunQty, 0) AS RetrunQty,
                                 IFNULL(POSReturnTable.CustomerRetrunQty, 0) AS CustomerRetrunQty,
                                 IFNULL(GoodIssue.IssueQty, 0) AS IssueQty,
                                 GrnHead.ItemID,
                                 GrnHead.FKLocationId
                             FROM
                                 (
                                     SELECT
                                         SUM(grnbody.Qty) AS PurchaseQty,
                                         grnbody.ItemID,
                                         grnHead.FKLocationId
                                     FROM
                                         TblGRNHead AS grnHead
                                         INNER JOIN TblGRNBody AS grnbody ON grnHead.Id = grnbody.Grnno
                                         AND grnHead.FKLocationId = grnbody.FKLocationId
                                     WHERE
                                         grnHead.IsDelete = FALSE
                                     GROUP BY
                                         grnbody.ItemID, grnHead.FKLocationId
                                 ) AS GrnHead
                                 LEFT OUTER JOIN (
                                     SELECT
                                         SUM(SRNbody.Qty) AS RetrunQty,
                                         SRNbody.ItemID,
                                         SRNHead.FKLocationId
                                     FROM
                                         TblStockReturnNoteHead AS SRNHead
                                         INNER JOIN TblStockReturnNoteBody AS SRNbody ON SRNHead.id = SRNbody.SRNno
                                         AND SRNHead.FKLocationId = SRNbody.FKLocationId
                                     WHERE
                                         SRNHead.IsDelete = FALSE
                                     GROUP BY
                                         SRNbody.ItemID,SRNHead.FKLocationId
                                 ) AS SRNHead ON GrnHead.ItemID = SRNHead.ItemID
                                 and GrnHead.FKLocationId = SRNHead.FKLocationId
                                 LEFT OUTER JOIN (
                                     SELECT
                                         SUM(posBody.Qty) AS SaleQty,
                                         posBody.ItemID,
                                         posHead.FKLocationId
                                     FROM
                                         TblPOSHead AS posHead
                                         INNER JOIN TblPOSBody AS posBody ON posHead.Id = posBody.POSNO
                                         AND posHead.FKLocationId = posBody.FKLocationId
                                     WHERE
                                         posHead.IsDelete = FALSE
                                     GROUP BY
                                         posBody.ItemID,posHead.FKLocationId
                                 ) AS PosHead ON GrnHead.ItemID = PosHead.ItemID
                                 and GrnHead.FKLocationId = PosHead.FKLocationId
                                 LEFT OUTER JOIN (
                                     SELECT
                                         SUM(GINBody.Qty) AS IssueQty,
                                         GINBody.ItemID,
                                         GINHead.FKLocationId
                                     FROM
                                         TblGINHead AS GINHead
                                         INNER JOIN TblGINBody AS GINBody ON GINHead.GINId = GINBody.GINId
                                         AND GINHead.FKLocationId = GINBody.FKLocationId
                                     WHERE
                                         GINHead.IsDelete = FALSE
                                     GROUP BY
                                         GINBody.ItemID,GINHead.FKLocationId
                                 ) AS GoodIssue ON GrnHead.ItemID = GoodIssue.ItemID
                                 and GrnHead.FKLocationId = GoodIssue.FKLocationId
                                 LEFT OUTER JOIN (
                                     SELECT
                                         SUM(POSRetrunbody.Qty) AS CustomerRetrunQty,
                                         POSRetrunbody.ItemID,
                                         POSReturnHead.FKLocationId
                                     FROM
                                         TblPOSReturnHead AS POSReturnHead
                                         INNER JOIN TblPOSReturnBody AS POSRetrunbody ON POSReturnHead.id = POSRetrunbody.POSReturnNO
                                         AND POSReturnHead.FKLocationId = POSRetrunbody.FKLocationId
                                     WHERE
                                         POSReturnHead.IsDelete = FALSE
                                     GROUP BY
                                         POSRetrunbody.ItemID,POSReturnHead.FKLocationId
                                 ) AS POSReturnTable ON PosHead.ItemID = POSReturnTable.ItemID
                                 and GrnHead.FKLocationId = POSReturnTable.FKLocationId
                         ) AS tbl ON ItemDetails.Id = tbl.ItemID AND    tbl.FKLocationId = LocationDetails.ID
        
                     )
                 WHERE
                     IFNULL(
                         (tbl.PurchaseQty + tbl.CustomerRetrunQty) -(tbl.RetrunQty + tbl.SaleQty),
                         0
                     ) < ItemDetails.ReorderLevel
                 ORDER BY
                     ItemDetails.Id,
                     ItemDetails.CategoryName";

        public string VWAllActiveANDAvailableItemList = @"CREATE VIEW VWAllActiveANDAvailableItemList AS 

                SELECT
                    ItemDetails.ReorderLevel,
                    IFNULL(
                        (tbl.PurchaseQty + tbl.CustomerRetrunQty) -(tbl.RetrunQty + tbl.SaleQty + tbl.IssueQty),
                        0
                    ) AS BalanceQty,
                    ItemDetails.MinLevel,
                    ItemDetails.MaxLevel,
                    ItemDetails.ItemName,
                    ItemDetails.ItemDescription,
                    ItemDetails.LastPurchasePrice,
                    ItemDetails.SellingPrice,
                    ItemDetails.ItemCode,
                    ItemDetails.CategoryName,
                    ItemDetails.FkCategoryId,
                    ItemDetails.FkBrandId,
                    ItemDetails.ImageUrl,
                    ItemDetails.BrandName,
                    ItemDetails.UnitName,
                    ItemDetails.UnitSize,
                    ItemDetails.FkUnitId,
                    ItemDetails.Id,
                    ItemDetails.IsDelete,
                     LocationDetails.LocationName,
                    LocationDetails.ID AS LocationId
                FROM
                    (
                        (
                            SELECT
                                item.ReorderLevel,
                                item.MinLevel,
                                item.MaxLevel,
                                item.UnitSize,
                                item.FkUnitId,
                                item.ItemName,
                                item.ItemDescription,
                                item.IsDelete,
                                item.LastPurchasePrice,
                                item.SellingPrice,
                                item.ItemCode,
                                item.FkCategoryId,
                                item.ImageUrl,
                                Category.Name AS CategoryName,
                                item.FkBrandId,
                                brand.Name AS BrandName,
                                Units.Name AS UnitName,
                                item.Id
                            FROM
                                TblStock_Main AS item
                                INNER JOIN TblItemCategories AS Category ON item.FkCategoryId = Category.Id
                                INNER JOIN TblItemBrandNames AS brand ON item.FkBrandId = brand.Id
                                INNER JOIN TblItemUnits AS Units ON item.FkUnitId = Units.Id
                            WHERE
                                item.IsDelete = FALSE
                        ) AS ItemDetails
                     JOIN (
                            SELECT
                                 location.ID,
                                 location.companyName AS LocationName
                             FROM
                                 TblCompanyDetails AS location
                        ) AS LocationDetails
        
                        LEFT OUTER JOIN (
                            SELECT
                                IFNULL(GrnHead.PurchaseQty, 0) AS PurchaseQty,
                                IFNULL(PosHead.SaleQty, 0) AS SaleQty,
                                IFNULL(SRNHead.RetrunQty, 0) AS RetrunQty,
                                IFNULL(POSReturnTable.CustomerRetrunQty, 0) AS CustomerRetrunQty,
                                IFNULL(GoodIssue.IssueQty, 0) AS IssueQty,
                                GrnHead.ItemID,
                                GrnHead.FKLocationId
                            FROM
                                (
                                    SELECT
                                        SUM(grnbody.Qty) AS PurchaseQty,
                                        grnbody.ItemID,
                                        grnHead.FKLocationId
                                    FROM
                                        TblGRNHead AS grnHead
                                        INNER JOIN TblGRNBody AS grnbody ON grnHead.Id = grnbody.Grnno
                                        AND grnHead.FKLocationId = grnbody.FKLocationId
                                    WHERE
                                        grnHead.IsDelete = FALSE
                                    GROUP BY
                                        grnbody.ItemID,grnHead.FKLocationId
                                ) AS GrnHead
                                LEFT OUTER JOIN (
                                    SELECT
                                        SUM(SRNbody.Qty) AS RetrunQty,
                                        SRNbody.ItemID,
                                        SRNHead.FKLocationId
                                    FROM
                                        TblStockReturnNoteHead AS SRNHead
                                        INNER JOIN TblStockReturnNoteBody AS SRNbody ON SRNHead.id = SRNbody.SRNno
                                        AND SRNHead.FKLocationId = SRNbody.FKLocationId
                                    WHERE
                                        SRNHead.IsDelete = FALSE
                                    GROUP BY
                                        SRNbody.ItemID,SRNHead.FKLocationId
                                ) AS SRNHead ON GrnHead.ItemID = SRNHead.ItemID
                                and GrnHead.FKLocationId = SRNHead.FKLocationId
                                LEFT OUTER JOIN (
                                    SELECT
                                        SUM(posBody.Qty) AS SaleQty,
                                        posBody.ItemID,
                                        posHead.FKLocationId
                                    FROM
                                        TblPOSHead AS posHead
                                        INNER JOIN TblPOSBody AS posBody ON posHead.Id = posBody.POSNO
                                        AND posHead.FKLocationId = posBody.FKLocationId
                                    WHERE
                                        posHead.IsDelete = FALSE
                                    GROUP BY
                                        posBody.ItemID,  posHead.FKLocationId
                                ) AS PosHead ON GrnHead.ItemID = PosHead.ItemID
                                and GrnHead.FKLocationId = PosHead.FKLocationId
                                LEFT OUTER JOIN (
                                    SELECT
                                        SUM(GINBody.Qty) AS IssueQty,
                                        GINBody.ItemID,
                                        GINHead.FKLocationId
                                    FROM
                                        TblGINHead AS GINHead
                                        INNER JOIN TblGINBody AS GINBody ON GINHead.GINId = GINBody.GINId
                                        AND GINHead.FKLocationId = GINBody.FKLocationId
                                    WHERE
                                        GINHead.IsDelete = FALSE
                                    GROUP BY
                                        GINBody.ItemID,GINHead.FKLocationId
                                ) AS GoodIssue ON GrnHead.ItemID = GoodIssue.ItemID
                                and GrnHead.FKLocationId = GoodIssue.FKLocationId
                                LEFT OUTER JOIN (
                                    SELECT
                                        SUM(POSRetrunbody.Qty) AS CustomerRetrunQty,
                                        POSRetrunbody.ItemID,
                                        POSReturnHead.FKLocationId
                                    FROM
                                        TblPOSReturnHead AS POSReturnHead
                                        INNER JOIN TblPOSReturnBody AS POSRetrunbody ON POSReturnHead.id = POSRetrunbody.POSReturnNO
                                        AND POSReturnHead.FKLocationId = POSRetrunbody.FKLocationId
                                    WHERE
                                        POSReturnHead.IsDelete = FALSE
                                    GROUP BY
                                        POSRetrunbody.ItemID,POSReturnHead.FKLocationId
                                ) AS POSReturnTable ON PosHead.ItemID = POSReturnTable.ItemID
                                and GrnHead.FKLocationId = POSReturnTable.FKLocationId
                        ) AS tbl ON ItemDetails.Id = tbl.ItemID AND    tbl.FKLocationId = LocationDetails.ID
                    )";

        public string VWAllActiveANDAvailableItemListForSupplierreturn = @"CREATE VIEW VWAllActiveANDAvailableItemListForSupplierreturn AS 
            SELECT
                ItemDetails.ReorderLevel,
                tbl.GRNRefInv,
                tbl.SRNRefInv,
                tbl.PurchaseQty as PurchaseQuantity,
                tbl.RetrunQty,
                IFNULL(
                    (tbl.PurchaseQty + tbl.CustomerRetrunQty) -(tbl.RetrunQty + tbl.SaleQty + tbl.IssueQty),
                    0
                ) AS BalanceQty,
                ItemDetails.MinLevel,
                ItemDetails.MaxLevel,
                ItemDetails.ItemName,
                ItemDetails.ItemDescription,
                ItemDetails.LastPurchasePrice,
                ItemDetails.SellingPrice,
                ItemDetails.ItemCode,
                ItemDetails.CategoryName,
                ItemDetails.FkCategoryId,
                ItemDetails.FkBrandId,
                ItemDetails.ImageUrl,
                ItemDetails.BrandName,
                ItemDetails.UnitName,
                ItemDetails.UnitSize,
                ItemDetails.FkUnitId,
                ItemDetails.Id,
                ItemDetails.IsDelete,
                LocationDetails.LocationName,
                LocationDetails.ID AS LocationID
            FROM
                (
                    (
                        SELECT
                            item.ReorderLevel,
                            item.MinLevel,
                            item.MaxLevel,
                            item.UnitSize,
                            item.FkUnitId,
                            item.ItemName,
                            item.ItemDescription,
                            item.IsDelete,
                            item.LastPurchasePrice,
                            item.SellingPrice,
                            item.ItemCode,
                            item.FkCategoryId,
                            item.ImageUrl,
                            Category.Name AS CategoryName,
                            item.FkBrandId,
                            brand.Name AS BrandName,
                            Units.Name AS UnitName,
                            item.Id
                        FROM
                            TblStock_Main AS item
                            INNER JOIN TblItemCategories AS Category ON item.FkCategoryId = Category.Id
                            INNER JOIN TblItemBrandNames AS brand ON item.FkBrandId = brand.Id
                            INNER JOIN TblItemUnits AS Units ON item.FkUnitId = Units.Id
                            
                        WHERE
                            item.IsDelete = FALSE
                    ) AS ItemDetails
                    JOIN (
                       SELECT
                             location.ID,
                             location.companyName AS LocationName
                         FROM
                             TblCompanyDetails AS location
                    ) AS LocationDetails
                    LEFT OUTER JOIN (
                        SELECT
                            IFNULL(GrnHead.PurchaseQty, 0) AS PurchaseQty,
                            IFNULL(PosHead.SaleQty, 0) AS SaleQty,
                            IFNULL(SRNHead.RetrunQty, 0) AS RetrunQty,
                            IFNULL(POSReturnTable.CustomerRetrunQty, 0) AS CustomerRetrunQty,
                            IFNULL(GoodIssue.IssueQty, 0) AS IssueQty,
                            GrnHead.RefInv AS GRNRefInv,
                            SRNHead.RefInv AS SRNRefInv,
                            GrnHead.ItemID, GrnHead.FKLocationId
                        FROM
                            (
                                SELECT
                                    SUM(grnbody.Qty) AS PurchaseQty,
                                    grnHead.RefInv,
                                    grnbody.ItemID,
                                    grnHead.FKLocationId
                                FROM
                                    TblGRNHead AS grnHead
                                    INNER JOIN TblGRNBody AS grnbody ON grnHead.Id = grnbody.Grnno
                                    AND grnHead.FKLocationId = grnbody.FKLocationId
                                WHERE
                                    grnHead.IsDelete = FALSE
                                GROUP BY
                                    grnbody.ItemID,
                                    grnHead.RefInv
                            ) AS GrnHead
                            LEFT OUTER JOIN (
                                SELECT
                                    SUM(SRNbody.Qty) AS RetrunQty,
                                    SRNHead.RefInv,
                                    SRNbody.ItemID, SRNHead.FKLocationId
                                FROM
                                    TblStockReturnNoteHead AS SRNHead
                                    INNER JOIN TblStockReturnNoteBody AS SRNbody ON SRNHead.id = SRNbody.SRNno
                                     AND SRNHead.FKLocationId = SRNbody.FKLocationId
                                WHERE
                                    SRNHead.IsDelete = FALSE
                                GROUP BY
                                    SRNbody.ItemID,
                                    SRNHead.RefInv
                            ) AS SRNHead ON GrnHead.RefInv = SRNHead.RefInv
                             and GrnHead.FKLocationId = SRNHead.FKLocationId
                            LEFT OUTER JOIN (
                                SELECT
                                    SUM(posBody.Qty) AS SaleQty,
                                    posBody.ItemID, posHead.FKLocationId
                                FROM
                                    TblPOSHead AS posHead
                                    INNER JOIN TblPOSBody AS posBody ON posHead.Id = posBody.POSNO
                                    AND posHead.FKLocationId = posBody.FKLocationId
                                WHERE
                                    posHead.IsDelete = FALSE
                                GROUP BY
                                    posBody.ItemID
                            ) AS PosHead ON GrnHead.ItemID = PosHead.ItemID
                            and GrnHead.FKLocationId = PosHead.FKLocationId
                            LEFT OUTER JOIN (
                                SELECT
                                    SUM(GINBody.Qty) AS IssueQty,
                                    GINBody.ItemID,
                                    GINHead.FKLocationId
                                FROM
                                    TblGINHead AS GINHead
                                    INNER JOIN TblGINBody AS GINBody ON GINHead.GINId = GINBody.GINId
                                    AND GINHead.FKLocationId = GINBody.FKLocationId
                                WHERE
                                    GINHead.IsDelete = FALSE
                                GROUP BY
                                    GINBody.ItemID
                            ) AS GoodIssue ON GrnHead.ItemID = GoodIssue.ItemID
                            and GrnHead.FKLocationId = GoodIssue.FKLocationId
                            LEFT OUTER JOIN (
                                SELECT
                                    SUM(POSRetrunbody.Qty) AS CustomerRetrunQty,
                                    POSRetrunbody.ItemID,POSReturnHead.FKLocationId
                                FROM
                                    TblPOSReturnHead AS POSReturnHead
                                    INNER JOIN TblPOSReturnBody AS POSRetrunbody ON POSReturnHead.id = POSRetrunbody.POSReturnNO
                                    AND POSReturnHead.FKLocationId = POSRetrunbody.FKLocationId
                                WHERE
                                    POSReturnHead.IsDelete = FALSE
                                GROUP BY
                                    POSRetrunbody.ItemID
                            ) AS POSReturnTable ON PosHead.ItemID = POSReturnTable.ItemID
                             and GrnHead.FKLocationId = POSReturnTable.FKLocationId
                    ) AS tbl ON ItemDetails.Id = tbl.ItemID AND    tbl.FKLocationId = LocationDetails.ID
                )";

        public string VwListPOSHeads = @"CREATE VIEW VwListPOSHeads AS 

                SELECT
                    posHead.Id,
                    posHead.Date as InvoiceDate,
                    posHead.Type,
                    posHead.Description,
                    posHead.RefInv,
                    posHead.Created,
                    posHead.FKClientId,
                    posHead.Total,
                    posHead.Discount,
                    posHead.Gross,
                    posHead.IsDelete,
                    posHead.Edit_By,
                    posHead.Edit_Date,
                    posHead.Delete_Date,
                    posHead.Delete_By,
                    u.FirstName AS UserName,
                    CONCAT(c.FirstName, ' ', c.LastName) AS Customer,
                    location.companyName AS locationName,
	                 location.ID AS LocationId
                FROM
                    TblPOSHead AS posHead
                    INNER JOIN TblUsers AS u ON posHead.Created = u.id
                    INNER JOIN TblClients AS c ON posHead.FKClientId = c.ID
                    JOIN TblCompanyDetails AS location ON  posHead.FKLocationId= location.ID";

        public string VwListPOSReturnHeads = @"CREATE VIEW VwListPOSReturnHeads AS SELECT
                posHead.Id,
                posHead.Date as InvoiceDate,
                posHead.`Type`,
                posHead.Description,
                posHead.RefInv,
                posHead.Created,
                posHead.FKClientId,
                posHead.Total,
                posHead.Discount,
                posHead.Gross,
                posHead.IsDelete,
                posHead.Edit_By,
                posHead.Edit_Date,
                posHead.Delete_Date,
                posHead.Delete_By,
                u.FirstName AS UserName,
                CONCAT(c.FirstName, ' ', c.LastName) AS Customer,
                location.companyName AS locationName,
            location.ID AS LocationId
            FROM
                TblPOSReturnHead AS posHead
                INNER JOIN TblUsers AS u ON posHead.Created = u.id
                INNER JOIN TblClients AS c ON posHead.FKClientId = c.ID
                JOIN TblCompanyDetails AS location ON  posHead.FKLocationId= location.ID";

        public string VwListSRNHeads = @"CREATE VIEW VwListSRNHeads AS SELECT
                  SrnHead.ID,
                SrnHead.Description,
                SrnHead.Date as InvoiceDate,
                SrnHead.`Type`,
                SrnHead.SRNType,
                SrnHead.RefInv,
                SrnHead.Created,
                SrnHead.FKSupplier_ID,
                SrnHead.Total,
                SrnHead.Discount,
                SrnHead.Gross,
                SrnHead.IsDelete,
                SrnHead.Edit_By,
                SrnHead.Edit_Date,
                SrnHead.Delete_By,
                SrnHead.Delete_Date,
                u.FirstName,
                s.Company AS Supplier,
                location.companyName AS locationName,
	             location.ID AS LocationId
            FROM
                TblStockReturnNoteHead AS SrnHead
                INNER JOIN TblUsers AS u ON SrnHead.Created = u.id
                INNER JOIN TblSupplier AS s ON SrnHead.FKSupplier_ID = s.id
                JOIN TblCompanyDetails AS location ON  SrnHead.FKLocationId= location.ID";

        public string ViewAllPOsInvoiceItem = @"CREATE VIEW ViewAllPOsInvoiceItem AS 
                
            SELECT
                POSGET.INVNo,
                POSGET.BodyId,
                POSGET.ItemID,
                POSGET.ItemName,
                POSGET.UnitName,
                POSGET.UnitSize,
                POSGET.Code,
                POSGET.Qty,
                POSGET.FreeQty,
                POSGET.UnitCost,
                POSGET.Cost,
                POSGET.Price,
                POSGET.DisCount,
                (IFNULL(POSGET.Qty, 0) - IFNULL(POSPUT.ReturnQTy, 0)) AS TotalQty,
                POSGET.UserName,
                IFNULL(POSPUT.ReturnQTY, 0) AS ReturnQTY,
                POSGET.LocationId,
                POSGET.locationName
            FROM
                (
                    SELECT
                PosBody.Id AS BodyId,
                REPLACE('INV000_', '_', POSHead.Id) AS INVNo,
                PosBody.ItemID,
                ItemMain.ItemName,
                ItemMain.UnitSize,
                unit.Name AS UnitName,
                PosBody.Code,
                SUM(PosBody.Qty) AS Qty,
                PosBody.FreeQty,
                PosBody.UnitCost,
                PosBody.Cost,
                PosBody.Price,
                PosBody.ExpDate,
                PosBody.DisCount,
                CONCAT(USERs.FirstName, ' ', USERs.LastName) AS UserName,
                location.companyName AS locationName,
                location.Id AS LocationId
            FROM
                TblPOSBody AS PosBody
                INNER JOIN TblStock_Main AS ItemMain ON PosBody.ItemID = ItemMain.id
                INNER JOIN TblItemUnits AS unit ON ItemMain.FkUnitId = unit.Id
                INNER JOIN TblPOSHead AS POSHead ON PosBody.POSNO = POSHead.Id
                INNER JOIN TblUsers AS USERs ON POSHead.Edit_By = USERs.Id
                INNER JOIN TblCompanyDetails AS location ON POSHead.FKLocationId = location.Id
            GROUP BY
                PosBody.Id,
                POSHead.Id,
                PosBody.ItemID,
                ItemMain.ItemName,
                ItemMain.UnitSize,
                unit.Name,
                PosBody.Code,
                PosBody.FreeQty,
                PosBody.UnitCost,
                PosBody.Cost,
                PosBody.Price,
                PosBody.ExpDate,
                PosBody.DisCount,
                USERs.FirstName,
                USERs.LastName,
                location.companyName,
                location.Id 

                ) AS POSGET 
                LEFT OUTER JOIN (
                   SELECT
                MAX(PosReturnBody.Id) AS Id,  -- Apply aggregate function for PosReturnBody.Id
                MAX(PosReturnBody.POSReturnNO) AS POSReturnNO,  -- Apply aggregate function for POSReturnNO
                POSReturnHead.RefInv AS INVNo,
                PosReturnBody.ItemID,
                ItemMain.ItemName,
                PosReturnBody.POSBodyKeyNo,
                MAX(PosReturnBody.Code) AS Code,  -- Apply aggregate function for PosReturnBody.Code
                SUM(PosReturnBody.Qty) AS ReturnQTy,
                MAX(PosReturnBody.FreeQty) AS FreeQty,  -- Apply aggregate function for PosReturnBody.FreeQty
                MAX(PosReturnBody.UnitCost) AS UnitCost,
                MAX(PosReturnBody.Cost) AS Cost,
                MAX(PosReturnBody.Price) AS Price,
                MAX(PosReturnBody.ExpDate) AS ExpDate,
                MAX(PosReturnBody.DisCount) AS DisCount,
                location.companyName AS locationName,
                location.Id AS PutLocationId
            FROM
                TblPOSReturnBody AS PosReturnBody
                INNER JOIN TblStock_Main AS ItemMain ON PosReturnBody.ItemID = ItemMain.id
                INNER JOIN TblPOSReturnHead AS POSReturnHead ON PosReturnBody.POSReturnNO = POSReturnHead.Id
                INNER JOIN TblCompanyDetails AS location ON POSReturnHead.FKLocationId = location.ID
            WHERE
                POSReturnHead.IsDelete = false
            GROUP BY
                POSReturnHead.RefInv,
                PosReturnBody.ItemID,
                PosReturnBody.POSBodyKeyNo,
                ItemMain.ItemName,
                location.companyName,
                location.Id

                ) AS POSPUT ON POSGET.INVNo = POSPUT.INVNo
                AND POSGET.ItemID = POSPUT.ItemID
                AND POSGET.BodyId = POSPUT.POSBodyKeyNo
    
                GROUP BY
                POSGET.INVNo,
                POSGET.BodyId,
                POSGET.ItemID,
                POSGET.ItemName,
                POSGET.UnitName,
                POSGET.UnitSize,
                POSGET.Code,
                POSGET.Qty,
                POSGET.FreeQty,
                POSGET.UnitCost,
                POSGET.Cost,
                POSGET.Price,
                POSGET.DisCount,
                POSGET.UserName,
                POSGET.locationName,
                POSGET.LocationId,
                POSPUT.INVNo,
                POSPUT.ItemID,
                POSPUT.POSBodyKeyNo,
                POSPUT.ItemName,
                POSPUT.locationName,
                POSPUT.PutLocationId
    

            ";

        public string ViewAllPOsInvoiceItemForReport = @"CREATE VIEW ViewAllPOsInvoiceItemForReport AS
            SELECT 
                POSGET.INVNo,
                POSGET.BodyId,
                POSGET.ItemID,
                POSGET.ItemName,
                POSGET.Code,
                POSGET.Qty,
                POSGET.FreeQty,
                POSGET.UnitCost,
                POSGET.Cost,
                POSGET.Price,
                POSGET.DisCount,
                (COALESCE(POSGET.Qty, 0) - COALESCE(POSPUT.ReturnQTy, 0)) AS TotalQty,
                POSGET.UserName,
                COALESCE(POSPUT.ReturnQTY, 0) AS ReturnQTY,
                POSGET.LocationId,
                POSGET.locationName
            FROM (
                SELECT 
                    PosBody.Id AS BodyId,
                    CONCAT('INV000_', POSHead.Id) AS INVNo,
                    PosBody.ItemID,
                    ItemMain.ItemName,
                    PosBody.Code,
                    SUM(PosBody.Qty) AS Qty,
                    PosBody.FreeQty,
                    PosBody.UnitCost,
                    PosBody.Cost,
                    PosBody.Price,
                    PosBody.ExpDate,
                    PosBody.DisCount,
                    CONCAT(Users.FirstName, ' ', Users.LastName) AS UserName,
                    location.companyName AS locationName,
                    location.ID AS LocationId
                FROM TblPOSBody AS PosBody
                INNER JOIN TblStock_Main AS ItemMain ON PosBody.ItemID = ItemMain.id
                INNER JOIN TblPOSHead AS POSHead ON PosBody.POSNO = POSHead.Id
                INNER JOIN TblUsers AS Users ON POSHead.Edit_By = Users.Id
                INNER JOIN TblCompanyDetails AS location ON POSHead.FKLocationId = location.ID
                WHERE PosBody.IsDelete = 0
                GROUP BY PosBody.Id, POSHead.Id, PosBody.ItemID, ItemMain.ItemName, PosBody.Code, 
                         PosBody.FreeQty, PosBody.UnitCost, PosBody.Cost, PosBody.Price, PosBody.ExpDate, 
                         PosBody.DisCount, Users.FirstName, Users.LastName, location.companyName, location.ID
            ) AS POSGET
            LEFT JOIN (
                SELECT 
                    PosReturnBody.POSBodyKeyNo,
                    POSReturnHead.RefInv AS INVNo,
                    PosReturnBody.ItemID,
                    SUM(PosReturnBody.Qty) AS ReturnQTy
                FROM TblPOSReturnBody AS PosReturnBody
                INNER JOIN TblPOSReturnHead AS POSReturnHead ON PosReturnBody.POSReturnNO = POSReturnHead.Id
                WHERE POSReturnHead.IsDelete = 0
                GROUP BY PosReturnBody.POSBodyKeyNo, POSReturnHead.RefInv, PosReturnBody.ItemID
            ) AS POSPUT 
            ON POSGET.INVNo = POSPUT.INVNo 
            AND POSGET.ItemID = POSPUT.ItemID 
            AND POSGET.BodyId = POSPUT.POSBodyKeyNo
            ORDER BY POSGET.Qty DESC;
";

        public string ViewAllPOsInvoiceItemForReportPage = @"CREATE VIEW ViewAllPOsInvoiceItemForReportPage AS 
                SELECT
                    POSGET.INVNo,
                    POSGET.BodyId,
                    POSGET.ItemID,
                    POSGET.ItemName,
                    POSGET.Code,
                    POSGET.Qty,
                    POSGET.FreeQty,
                    POSGET.UnitCost,
                    POSGET.Cost as SalesCost,
                    POSGET.Price,
                    POSGET.DisCount as SalesDisCount,
                    IFNULL( POSPUT.Cost,0) AS SalesReturn,
                    IFNULL( POSPUT.DisCount,0) AS ReturnDisCount,
                    (IFNULL( POSGET.Qty,0) -IFNULL(POSPUT.ReturnQTy,0)) AS TotalQty,
                    POSGET.UserName,
                    IFNULL(POSPUT.ReturnQTY,0) AS ReturnQTY,
                    POSGET.LocationId,
                    POSGET.Date,
                    POSGET.locationName
                FROM

        (
            SELECT
                location.ID,
                location.companyName AS LocationName
            FROM
                TblCompanyDetails AS location
        ) AS LocationDetails
        INNER JOIN 
                    (
                        SELECT
                            PosBody.Id AS BodyId,
                            REPLACE('INV000_', '_', POSHead.Id) AS INVNo,
                            PosBody.ItemID,
                            ItemMain.ItemName,
                            PosBody.Code,
                            SUM(PosBody.Qty) AS Qty,
                            PosBody.FreeQty,
                            PosBody.UnitCost,
                            POSHead.Date , 
                            PosBody.Cost,
                            PosBody.Price,
                            PosBody.ExpDate,
                            PosBody.DisCount,
                            PosBody.IsDelete,
                            CONCAT(USERs.FirstName, ' ', USERs.LastName) AS UserName,
                            location.companyName AS locationName,
				                location.ID AS LocationId
                        FROM
                            TblPOSBody AS PosBody
                            INNER JOIN TblStock_Main AS ItemMain ON PosBody.ItemID = ItemMain.id
                            INNER JOIN TblPOSHead AS POSHead ON PosBody.POSNO = POSHead.Id
                            INNER JOIN TblUsers AS USERs ON POSHead.Edit_By = USERs.Id
          	                INNER  JOIN TblCompanyDetails AS location ON  POSHead.FKLocationId= location.Id
                        GROUP BY
                           PosBody.Id,
								    POSHead.Id,
								    PosBody.ItemID,
								    ItemMain.ItemName,
								    ItemMain.UnitSize,
								    PosBody.Code,
								    PosBody.FreeQty,
								    PosBody.UnitCost,
								    PosBody.Cost,
								    PosBody.Price,
								    PosBody.ExpDate,
								    PosBody.DisCount,
								    USERs.FirstName,
								    USERs.LastName,
								    location.companyName,
								    location.Id 

                    ) AS POSGET ON LocationDetails.ID =POSGET.LocationId
                    LEFT OUTER JOIN (
                        SELECT
                            PosReturnBody.Id AS ID,
                            PosReturnBody.POSReturnNO,
                            POSReturnHead.RefInv AS INVNo,
                            PosReturnBody.ItemID,
                            ItemMain.ItemName,
                            PosReturnBody.POSBodyKeyNo,
                            PosReturnBody.Code,
                            SUM(PosReturnBody.Qty) AS ReturnQTy,
                            PosReturnBody.FreeQty,
                            PosReturnBody.UnitCost,
                            PosReturnBody.Cost,
                            PosReturnBody.Price,
                            PosReturnBody.ExpDate,
                            PosReturnBody.DisCount,
                            PosReturnBody.IsDelete,
                            location.companyName AS locationName,
				                location.ID AS PutLocationId
                        FROM
                            TblPOSReturnBody AS PosReturnBody
                            INNER JOIN TblStock_Main AS ItemMain ON PosReturnBody.ItemID = ItemMain.id
                            INNER JOIN TblPOSReturnHead AS POSReturnHead ON PosReturnBody.POSReturnNO = POSReturnHead.Id
          	                INNER  JOIN TblCompanyDetails AS location ON  POSReturnHead.FKLocationId= location.ID
                        WHERE
                            POSReturnHead.IsDelete = false
                        GROUP BY
                          POSReturnHead.RefInv,
							    PosReturnBody.ItemID,
							    PosReturnBody.POSBodyKeyNo,
							    ItemMain.ItemName,
							    location.companyName,
							    PosReturnBody.Id,
							    location.Id
                    ) AS POSPUT ON POSGET.INVNo = POSPUT.INVNo
                    AND POSGET.ItemID = POSPUT.ItemID
                    AND POSGET.BodyId = POSPUT.POSBodyKeyNo
                    
                    GROUP BY
    POSGET.INVNo,
    POSGET.BodyId,
    POSGET.ItemID,
    POSGET.ItemName,
    POSGET.Code,
    POSGET.Qty,
    POSGET.FreeQty,
    POSGET.UnitCost,
    POSGET.Cost,
    POSGET.Price,
    POSGET.DisCount,
    POSGET.locationName,
    POSGET.LocationId,
    POSPUT.INVNo,
    POSPUT.ItemID,
    POSPUT.POSBodyKeyNo,
    POSPUT.ItemName,
    POSPUT.locationName,
    POSPUT.PutLocationId,
    POSPUT.ID";

        public string VwAllPriceBackupHistory = @"CREATE VIEW VwAllPriceBackupHistory AS SELECT 
                            Category.Name AS CategoryName,
                            brand.Name AS BrandName,
                            MAX(Backuptable.LastPurchasePrice) AS LastPurchasePrice,
                            MAX(Backuptable.LastSellingPrice) AS LastSellingPrice,
                            MAX(Backuptable.NewPurchasePrice) AS NewPurchasePrice,
                            MAX(Backuptable.NewSellingPrice) AS NewSellingPrice,
                            Backuptable.FkCategoryId,
                            Backuptable.FkBrandId,
                            Backuptable.PriceChangeBackupDate,
                            MAX(Backuptable.PercentageLastPurchasePrice) AS PercentageLastPurchasePrice,
                            MAX(Backuptable.PercentageSellingPrice) AS PercentageSellingPrice
                        FROM TblPriceBackups AS Backuptable
                        INNER JOIN TblItemCategories AS Category ON Backuptable.FkCategoryId = Category.Id
                        INNER JOIN TblItemBrandNames AS brand ON Backuptable.FkBrandId = brand.Id
                        GROUP BY 
                            Backuptable.PriceChangeBackupDate,
                            Backuptable.FkCategoryId,
                            Backuptable.FkBrandId,
                            Category.Name,
                            brand.Name";

        public string ViewAllPurcheseAndRevenue = @"CREATE VIEW ViewAllPurcheseAndRevenue AS 
                WITH AllMonths AS (
                    SELECT
                        DATE_ADD(
                            '2024-01-01',
                            INTERVAL (a.a + (10 * b.a) + (100 * c.a)) MONTH
                        ) AS MonthDate
                    FROM
                        (
                            SELECT
                                0 AS a
                            UNION
                            ALL
                            SELECT
                                1
                            UNION
                            ALL
                            SELECT
                                2
                            UNION
                            ALL
                            SELECT
                                3
                            UNION
                            ALL
                            SELECT
                                4
                            UNION
                            ALL
                            SELECT
                                5
                            UNION
                            ALL
                            SELECT
                                6
                            UNION
                            ALL
                            SELECT
                                7
                            UNION
                            ALL
                            SELECT
                                8
                            UNION
                            ALL
                            SELECT
                                9
                        ) a
                        JOIN (
                            SELECT
                                0 AS a
                            UNION
                            ALL
                            SELECT
                                1
                            UNION
                            ALL
                            SELECT
                                2
                            UNION
                            ALL
                            SELECT
                                3
                            UNION
                            ALL
                            SELECT
                                4
                            UNION
                            ALL
                            SELECT
                                5
                            UNION
                            ALL
                            SELECT
                                6
                            UNION
                            ALL
                            SELECT
                                7
                            UNION
                            ALL
                            SELECT
                                8
                            UNION
                            ALL
                            SELECT
                                9
                        ) b
                        JOIN (
                            SELECT
                                0 AS a
                            UNION
                            ALL
                            SELECT
                                1
                            UNION
                            ALL
                            SELECT
                                2
                            UNION
                            ALL
                            SELECT
                                3
                            UNION
                            ALL
                            SELECT
                                4
                            UNION
                            ALL
                            SELECT
                                5
                            UNION
                            ALL
                            SELECT
                                6
                            UNION
                            ALL
                            SELECT
                                7
                            UNION
                            ALL
                            SELECT
                                8
                            UNION
                            ALL
                            SELECT
                                9
                        ) c
                ),
                purchase AS (
                    SELECT
                        YEAR(`AllMonths`.`MonthDate`) AS `CurrentYear`,
                        MONTHNAME(`AllMonths`.`MonthDate`) AS `purcheseMONTH`,
                        COALESCE(
                            SUM((`grnBody`.`Cost` - `grnBody`.`DisCount`)),
                            0
                        ) AS `PurcheseCost`,
                        LocationDetails.LocationName,
                        LocationDetails.LocationId
                    FROM
                        (
                            (
                                AllMonths
                                JOIN (
                                    SELECT
                                        location.ID AS LocationId,
                                        location.CompanyName AS LocationName
                                    FROM
                                        TblCompanyDetails AS location
                                ) AS LocationDetails
                                LEFT JOIN `TblGRNHead` `GRNHead` ON(
                                    (
                                        YEAR(`AllMonths`.`MonthDate`) = YEAR(`GRNHead`.`Date`)
                                    )
                                    AND (
                                        MONTH(`AllMonths`.`MonthDate`) = MONTH(`GRNHead`.`Date`)
                                    )
                                )
                            )
                            LEFT JOIN `TblGRNBody` `grnBody` ON(
                                (
                                    (`GRNHead`.`Id` = `grnBody`.`Grnno`)
                                    AND (`GRNHead`.FKLocationId = grnBody.FKLocationId)
                                    AND (`GRNHead`.`IsDelete` = FALSE)
                                )
                            )
                            AND LocationDetails.LocationId = `GRNHead`.FKLocationId
                        )
                    WHERE
                        (YEAR(`AllMonths`.`MonthDate`) = YEAR(CURDATE()))
                    GROUP BY
                        `purcheseMONTH`,
                        `CurrentYear`,
                        LocationId,
                        AllMonths.MonthDate
                    ORDER BY
                        `CurrentYear`,
                        MONTH(`AllMonths`.`MonthDate`)
                ),

                Sales AS (
                    SELECT
                        YEAR(AllMonths.MonthDate) AS CurrentYear,
                        MONTHNAME(AllMonths.MonthDate) AS SaleMONTH,
                        COALESCE(
                            SUM((PosBody.Cost - PosBody.DisCount)),
                            0
                        ) AS SalesCost,
                        LocationDetails.LocationName,
                        LocationDetails.LocationId
                    FROM
                        (
                            (
                                AllMonths
                                JOIN (
                                    SELECT
                                        location.ID AS LocationId,
                                        location.CompanyName AS LocationName
                                    FROM
                                        TblCompanyDetails AS location
                                ) AS LocationDetails
                                LEFT JOIN TblPOSHead POSHead ON(
                                    (
                                        YEAR(AllMonths.MonthDate) = YEAR(POSHead.Date)
                                    )
                                    AND (
                                        MONTH(AllMonths.MonthDate) = MONTH(POSHead.Date)
                                    )
                                )
                            )
                            LEFT JOIN TblPOSBody PosBody ON(
                                (
                                    (POSHead.Id = PosBody.POSNO)
                                    AND (POSHead.FKLocationId = PosBody.FKLocationId)
                                    AND (POSHead.IsDelete = FALSE)
                                )
                            )
                            AND LocationDetails.LocationId = POSHead.FKLocationId
                        )
                    WHERE
                        (YEAR(AllMonths.MonthDate) = YEAR(CURDATE()))
                    GROUP BY
                        SaleMONTH,
                        CurrentYear,
                        LocationId,
                        AllMonths.MonthDate
                    ORDER BY
                        CurrentYear,
                        MONTH(AllMonths.MonthDate)
                ),
                SReturn AS (
                    SELECT
                        YEAR(AllMonths.MonthDate) AS CurrentYear,
                        MONTHNAME(AllMonths.MonthDate) AS SReturnMONTH,
                        COALESCE(
                            SUM((SrnBody.Cost - SrnBody.DisCount)),
                            0
                        ) AS SReturnCost,
                        LocationDetails.LocationName,
                        LocationDetails.LocationId
                    FROM
                        (
                            (
                                AllMonths
                                JOIN (
                                    SELECT
                                        location.ID AS LocationId,
                                        location.CompanyName AS LocationName
                                    FROM
                                        TblCompanyDetails AS location
                                ) AS LocationDetails
                                LEFT JOIN TblStockReturnNoteHead SRNHead ON(
                                    (
                                        YEAR(AllMonths.MonthDate) = YEAR(SRNHead.Date)
                                    )
                                    AND (
                                        MONTH(AllMonths.MonthDate) = MONTH(SRNHead.Date)
                                    )
                                )
                            )
                            LEFT JOIN TblStockReturnNoteBody SrnBody ON(
                                (
                                    (SRNHead.Id = SrnBody.SRNno)
                                    AND (SRNHead.FKLocationId = SrnBody.FKLocationId)
                                    AND (SRNHead.IsDelete = FALSE)
                                )
                            )
                            AND LocationDetails.LocationId = SRNHead.FKLocationId
                        )
                    WHERE
                        (YEAR(AllMonths.MonthDate) = YEAR(CURDATE()))
                    GROUP BY
                        SReturnMONTH,
                        CurrentYear,
                        LocationId,
                        AllMonths.MonthDate
                    ORDER BY
                        CurrentYear,
                        MONTH(AllMonths.MonthDate)
                ),
                POSReturn AS (
                    SELECT
                        YEAR(AllMonths.MonthDate) AS CurrentYear,
                        MONTHNAME(AllMonths.MonthDate) AS PReturnMONTH,
                        COALESCE(
                            SUM((PrnBody.Cost - PrnBody.DisCount)),
                            0
                        ) AS PReturnCost,
                        LocationDetails.LocationName,
                        LocationDetails.LocationId
                    FROM
                        (
                            (
                                AllMonths
                                JOIN (
                                    SELECT
                                        location.ID AS LocationId,
                                        location.CompanyName AS LocationName
                                    FROM
                                        TblCompanyDetails AS location
                                ) AS LocationDetails
                                LEFT JOIN TblPOSReturnHead PRNHead ON(
                                    (
                                        YEAR(AllMonths.MonthDate) = YEAR(PRNHead.Date)
                                    )
                                    AND (
                                        MONTH(AllMonths.MonthDate) = MONTH(PRNHead.Date)
                                    )
                                )
                            )
                            LEFT JOIN TblPOSReturnBody PrnBody ON(
                                (
                                    (PRNHead.Id = PrnBody.POSReturnNO)
                                    AND (PRNHead.FKLocationId = PrnBody.FKLocationId)
                                    AND (PRNHead.IsDelete = FALSE)
                                )
                            )
                            AND LocationDetails.LocationId = PRNHead.FKLocationId
                        )
                    WHERE
                        (YEAR(AllMonths.MonthDate) = YEAR(CURDATE()))
                    GROUP BY
                        PReturnMONTH,
                        CurrentYear,
                        LocationId,
                        AllMonths.MonthDate
                    ORDER BY
                        CurrentYear,
                        MONTH(AllMonths.MonthDate)
                ),
                GIN AS (
                    SELECT
                        YEAR(AllMonths.MonthDate) AS CurrentYear,
                        MONTHNAME(AllMonths.MonthDate) AS GINMONTH,
                        COALESCE(
                            SUM((GInBody.Cost - GInBody.DisCount)),
                            0
                        ) AS GINCost,
                        LocationDetails.LocationName,
                        LocationDetails.LocationId
                    FROM
                        (
                            (
                                AllMonths
                                JOIN (
                                    SELECT
                                        location.ID AS LocationId,
                                        location.CompanyName AS LocationName
                                    FROM
                                        TblCompanyDetails AS location
                                ) AS LocationDetails
                                LEFT JOIN TblGINHead GINHead ON(
                                    (
                                        YEAR(AllMonths.MonthDate) = YEAR(GINHead.Date)
                                    )
                                    AND (
                                        MONTH(AllMonths.MonthDate) = MONTH(GINHead.Date)
                                    )
                                )
                            )
                            LEFT JOIN TblGINBody GInBody ON(
                                (
                                    (GINHead.GINId = GInBody.GINId)
                                    AND (GINHead.FKLocationId = GInBody.FKLocationId)
                                    AND (GINHead.IsDelete = FALSE)
                                )
                            )
                            AND LocationDetails.LocationId = GINHead.FKLocationId
                        )
                    WHERE
                        (YEAR(AllMonths.MonthDate) = YEAR(CURDATE()))
                    GROUP BY
                        GINMONTH,
                        CurrentYear,
                        LocationId,
                        AllMonths.MonthDate
                    ORDER BY
                        CurrentYear,
                        MONTH(AllMonths.MonthDate)
                )

                SELECT
                    purchase.CurrentYear,
                    purchase.purcheseMONTH,
                    purchase.PurcheseCost,
                    purchase.LocationName,
                    purchase.LocationId,
                    Sales.SalesCost,
                    POSReturn.PReturnCost AS SalesReturnCost,
                    IFNULL((Sales.SalesCost - (POSReturn.PReturnCost + (purchase.PurcheseCost - SReturn.SReturnCost))), 0) AS NetProfit,
                    SReturn.SReturnCost AS PurcheseReturnCost
                FROM
                    purchase

                    INNER join Sales ON purchase.LocationId = Sales.LocationId
                    AND purchase.purcheseMONTH = Sales.SaleMONTH
    
                    INNER JOIN SReturn ON purchase.locationId = SReturn.LocationId
                    AND purchase.purcheseMONTH = SReturn.SReturnMONTH

 	                INNER JOIN POSReturn ON purchase.locationId = POSReturn.LocationId
                    AND purchase.purcheseMONTH = POSReturn.PReturnMONTH  
	  
                   INNER JOIN GIN ON purchase.locationId = GIN.LocationId
                    AND purchase.purcheseMONTH = GIN.GINMONTH   

";


        public void InsertViewQuery(MigrationBuilder migrationBuilder)
        {
            var dt = new SQlViewsClass();
            migrationBuilder.Sql(dt.VWListClient);
            migrationBuilder.Sql(dt.VWAllActiveItemList);
            migrationBuilder.Sql(dt.VwListSupplier);
            migrationBuilder.Sql(dt.VwListItemCategory);
            migrationBuilder.Sql(dt.VwListItemUnit);
            migrationBuilder.Sql(dt.VwListItemBrand);
            migrationBuilder.Sql(dt.VwListGINHead);
            migrationBuilder.Sql(dt.VWAllActiveItemReorderLevelList);
            migrationBuilder.Sql(dt.VwListGRNHeads);
            migrationBuilder.Sql(dt.VWAllActiveANDAvailableItemList);
            migrationBuilder.Sql(dt.VWAllActiveANDAvailableItemListForSupplierreturn);
            migrationBuilder.Sql(dt.VwBestCustomers);
            migrationBuilder.Sql(dt.VwListPOSReturnHeads);
            migrationBuilder.Sql(dt.VwListSRNHeads);
            migrationBuilder.Sql(dt.ViewAllPOsInvoiceItem);
            migrationBuilder.Sql(dt.ViewAllPOsInvoiceItemForReport);
            migrationBuilder.Sql(dt.ViewAllPOsInvoiceItemForReportPage);
            migrationBuilder.Sql(dt.VwAllPriceBackupHistory);
            migrationBuilder.Sql(dt.ViewAllPurcheseAndRevenue);
            migrationBuilder.Sql(dt.VwListPOSHeads);
        }


        public void DeleteViewQuery(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS VWListClient");
            migrationBuilder.Sql("DROP VIEW IF EXISTS VWAllActiveItemList");
            migrationBuilder.Sql("DROP VIEW IF EXISTS VwListSupplier");
            migrationBuilder.Sql("DROP VIEW IF EXISTS VwListItemCategory");
            migrationBuilder.Sql("DROP VIEW IF EXISTS VwListItemUnit");
            migrationBuilder.Sql("DROP VIEW IF EXISTS VwListItemBrand");
            migrationBuilder.Sql("DROP VIEW IF EXISTS VwListGINHead");
            migrationBuilder.Sql("DROP VIEW IF EXISTS VWAllActiveItemReorderLevelList");
            migrationBuilder.Sql("DROP VIEW IF EXISTS VwListGRNHeads");
            migrationBuilder.Sql("DROP VIEW IF EXISTS VWAllActiveANDAvailableItemList");
            migrationBuilder.Sql("DROP VIEW IF EXISTS VWAllActiveANDAvailableItemListForSupplierreturn");
            migrationBuilder.Sql("DROP VIEW IF EXISTS VwBestCustomers");
            migrationBuilder.Sql("DROP VIEW IF EXISTS VwListPOSReturnHeads");
            migrationBuilder.Sql("DROP VIEW IF EXISTS VwListSRNHeads");
            migrationBuilder.Sql("DROP VIEW IF EXISTS ViewAllPOsInvoiceItem");
            migrationBuilder.Sql("DROP VIEW IF EXISTS ViewAllPOsInvoiceItemForReport");
            migrationBuilder.Sql("DROP VIEW IF EXISTS ViewAllPOsInvoiceItemForReportPage");
            migrationBuilder.Sql("DROP VIEW IF EXISTS VwAllPriceBackupHistory");
            migrationBuilder.Sql("DROP VIEW IF EXISTS ViewAllPurcheseAndRevenue");
            migrationBuilder.Sql("DROP VIEW IF EXISTS VwListPOSHeads");
        }
    }
}