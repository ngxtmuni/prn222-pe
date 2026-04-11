# TEST-QUESTION

Bo nay gom 7 de luyen theo format PRN222 Ha Noi.

## Cau truc chung

- Moi de nam trong `PaperNo_x`.
- `1/` la cau 1 console.
- `2/` la cau 2 Razor Pages.
- `question.md` la de tong.
- `1/question-q1.md` la mo ta rieng cho cau 1.
- `2/database.sql` la script DB.
- `2/*.html` la UI mau cho Razor Pages.

## Tong quan 7 de

| Paper | Q1 type | Domain | Q2 type | Tai nguyen di kem |
|---|---|---|---|---|
| PaperNo_1 | Console Client | Customer - Orders | List + Detail | ServerApp |
| PaperNo_2 | Console Server | Broker - Contract | List + Create | ClientApp |
| PaperNo_3 | Console Client | Service - Room | Search/List + Detail | ServerApp |
| PaperNo_4 | Console App | Employee - Department | List + Update | None |
| PaperNo_5 | Console Server | Product - Category | List + Create | ClientApp |
| PaperNo_6 | Console App | Student - Course | List + Detail | None |
| PaperNo_7 | Console App | Book - Author | List + Update | None |

## Thu tu luyen de nghi

1. `PaperNo_4`
2. `PaperNo_1`
3. `PaperNo_6`
4. `PaperNo_3`
5. `PaperNo_7`
6. `PaperNo_2`
7. `PaperNo_5`

Ly do:
- Bat dau bang bai console app thuan va Q2 don gian de quen nhom filter, sort, detail, update.
- Chuyen sang bai console client de luyen `TcpClient`, doc stream, deserialize JSON.
- Ket thuc bang bai console server de luyen `TcpListener`, command parsing va tra JSON.

## Cach luyen nhanh

### Muc tieu 1: On sat de Ha Noi trong thoi gian ngan

- Lam `PaperNo_1`, `PaperNo_3`, `PaperNo_2`, `PaperNo_5`.
- Day la nhom socket client/server sat dang Ha Noi nhat.

### Muc tieu 2: On nhanh Q2 Razor Pages

- Lam `PaperNo_1`, `PaperNo_2`, `PaperNo_4`, `PaperNo_7`.
- Nhom nay phu du `List`, `Detail`, `Create`, `Update`, `Filter`, `Search`.

### Muc tieu 3: On logic console truoc roi moi sang socket

- Lam `PaperNo_4`, `PaperNo_6`, `PaperNo_7` truoc.
- Sau do chuyen sang `PaperNo_1`, `PaperNo_3`, `PaperNo_2`, `PaperNo_5`.

## Cach dung cac de socket

### Neu Q1 la Console Client

- Mo folder `1/ServerApp`.
- Chay server truoc.
- Sau do tu code client theo `question-q1.md`.
- Uu tien pattern SP26: nhap `ALL` hoac nhap 1 gia tri loc duy nhat nhu city, category, property type, fee type.

### Neu Q1 la Console Server

- Tu code server theo `question-q1.md`.
- Mo folder `1/ClientApp` de test command va response.
- Uu tien pattern SP26: server nhan `ALL` hoac 1 gia tri loc duy nhat, va tra ve mot loai du lieu chinh.

## Ghi chu

- Cac `ServerApp`/`ClientApp` duoc tao theo style gan voi de Ha Noi va SP26.
- Client app duoc bo sung parse JSON co cau truc ro hon, nhung van co the xem raw response neu muon.
- Cac de Q2 co khoi luong duoc giu o muc hop ly cho bai thi 90 phut, tranh full CRUD.
