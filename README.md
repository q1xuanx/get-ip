# Get IP API

A lightweight **.NET API** to retrieve the **IP address** and **Port** of the client making the request.

## 🔑 Authentication

This API requires a **Bearer Token** for every request.
Include the token in the `Authorization` header:
### Example Request with cURL
```bash
curl -H "Authorization: Bearer <your_token_here>" \
     http://localhost:<port>/api/current
```

## 🚀 Endpoint
### `GET /api/current`

Returns the client IP address and port number.

#### Example Response
##### Success
```json
{
  "message": "SUCCESS",
  "ip": "127.0.0.1",
  "port": "5000"
}
```
##### Fail
```json
{
  "message": "Not found your ip address, please check again",
  "ip": "",
  "port": ""
}
```
```json
{
  "message": "Can not cast data into json type, please try again",
  "ip": "",
  "port": ""
}
```

