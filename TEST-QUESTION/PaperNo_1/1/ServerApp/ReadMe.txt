CustomerOrderServer

Run project: CustomerOrderServer
Host: 127.0.0.1
Port: 5101

Supported commands:
- ALL
- <city>

Examples:
- ALL
- Ha Noi
- Da Nang

Response is always JSON.

Sample response for Ha Noi:
{
  "Success": true,
  "Message": "Found 2 customer(s) in Ha Noi",
  "Count": 2,
  "Data": [
    {
      "CustomerId": 1,
      "CustomerName": "Nguyen Van A",
      "Email": "a@gmail.com",
      "City": "Ha Noi"
    }
  ]
}
