
# Transaction Vilidation

This assignment for my job interview assignment.
I was failed the interview for that time.  
For now, I fixed this assignment.


## 🚀 About Me
I'm Software Engineer(.Net) and I'm from Myanmar.

## About API 

#### submittrxmessage

```http
  [Domain]/api/submittrxmessage 
```

| Fieldname     | Type      | Size  | Required  | Description                |
| :--------     | :-------  | :---- | :-------  | :------------------------- |
| `partnerkey`  | `string`  | `50`  | `M`       | The allowed partner's key |
| `partnerrefno`  | `string`  | `50`  | `M`       | Partner's reference number for this transaction.|
| `totalamount`  | `Long`  | `-`  | `O`       | Total amount of payment in the MYR only. |
| `items`  | `Array of ` *`itemdetail`*  | `-`  | `O`       | Array of items purchased through this transaction. |
| `timestamp`  | `String`  |   | `M`       | String representation of the UTC time of the operation in ISO 8601 format. ie. 2013-11-22T02:11:22.0000000Z |
| `sig`  |   |   | `M`       | Message Signature Parameter Order `timestamp + partnerkey + partnerrefno + totalamount  + partnerpassword`, and use `SHA256` to encode the results in `Base64`|

##### *`itemdetail`* definition (`items`)

| Fieldname     | Type      | Size  | Required  | Description                |
| :--------     | :-------  | :---- | :-------  | :------------------------- |
| `partneritemref`  | `string`  | `50`  | `O`       | Reference ID the partner uses for this item. |
| `name `  | `string`  | `100`  | `O`       | Name of the item. |
| `qty `  | `Integer`  | `-`  | `O`       | Quantity of the item bought. |
| `unitprice`  | `Long`  | `-`  | `O`       | Price of one unit of the item in the currency of the transaction. |

#### Response Message

| Fieldname     | Type      | Size  | Required  | Description                |
| :--------     | :-------  | :---- | :-------  | :------------------------- |
| `result`  | `Integer`  | `-`  | `M`       | Whether the operation was successful or not. `1 is successful, 0 if errors encountered. ` |
| `resultmessage `  | `string`  | `-`  | `M`       | Result message if the operation was a Success or Failure. |

