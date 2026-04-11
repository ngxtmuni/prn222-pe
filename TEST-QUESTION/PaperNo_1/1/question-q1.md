# Q1 - Customer Orders Client

- Project type: Console client.
- Server host: `127.0.0.1`
- Server port: `5101`
- Response format: JSON.

Commands:
- `ALL`
- `Ha Noi`
- `Da Nang`
- `Hai Phong`
- `EXIT`

Expected behavior:
- Read command from keyboard.
- Connect to server.
- Send command.
- Read full response until server closes the write side.
- Deserialize valid JSON response to customer list response.
- Handle invalid command and connection errors.
