ServiceRoomServer

Run project: ServiceRoomServer
Host: 127.0.0.1
Port: 5303

Supported commands:
- ALL
- <feeType>

Response format: JSON object with `Success`, `Message`, `Count`, `Data`.

Sample response for Monthly:
{
  "Success": true,
  "Message": "Found 2 service(s) with fee type 'Monthly'.",
  "Count": 2,
  "Data": [
    {
      "ServiceId": 2,
      "RoomTitle": "Family Room",
      "FeeType": "Monthly",
      "Price": 420.0,
      "Status": "Occupied",
      "Description": "Room for family stay"
    },
    {
      "ServiceId": 3,
      "RoomTitle": "VIP Room",
      "FeeType": "Monthly",
      "Price": 650.0,
      "Status": "Available",
      "Description": "Large premium room"
    }
  ]
}
