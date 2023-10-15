<h1>StarkMOBA</h1>

StarkMoba is an server authoritative and both server & client side optimized MOBA Game Template.

<h3>About</h3>

[<img src="https://img.shields.io/badge/StarkSharp Version-0.3-green">](https://starksharp.com)
[<img src="https://img.shields.io/badge/Join-Telegram-blue">](https://t.me/starksharp)

<h3>Supported platforms</h3>

[<img src="https://img.shields.io/badge/.NET-4+-green">](./StarkSharp/StarkSharp.Docs/Platforms/DotNet/Setup.md)
[<img src="https://img.shields.io/badge/Unity-2020 LTS+-green">](./StarkSharp/StarkSharp.Docs/Platforms/Unity/Setup.md)

<h3>How To Connect Starknet</h3>

Check StarkSharp Installation

Example Connection:

1. Create a new platform

```
Platform myNewPlatform = UnityPlatform.New(PlatformConnectorType.Sharpion);
```

2. Create a new connector and add your platform to the connector

```
Connector connector = new Connector(myNewPlatform);
```

3. Connect Wallet

- ArgentX

```
connector.ConnectWallet(WalletType.ArgentX,
    (successMessage) => OnWalletConnectionSuccess(successMessage),
    (errorMessage) => OnWalletConnectionError(errorMessage));
```

- Braavos

```
connector.ConnectWallet(WalletType.Braavos,
    (successMessage) => OnWalletConnectionSuccess(successMessage),
    (errorMessage) => OnWalletConnectionError(errorMessage));
```

4. Create Actions

```
public void OnWalletConnectionSuccess(string message)
{
    connector.DebugMessage("On Wallet Connection Success: " + message);
}

public void OnWalletConnectionError(string message)
{
    connector.DebugMessage("On Wallet Connection Error: " + message);
}
```

5. Send Transaction

```
connector.SendTransaction(
    ERC20Standart.TransferToken(sendTransactionContractAddress, sendTransactionRecipientAddress, amount),
    (successMessage) => OnSendTransactionSuccess(successMessage),
    (errorMessage) => OnSendTransactionError(errorMessage));
```

- If you are wondering how to send a custom transaction, here is how you can do it:

```
ContractInteraction myContractInteraction = new ContractInteraction(_ContractAdress, _EntryPoint, _CallData);
connector.SendTransaction(
    myContractInteraction,
    (successMessage) => OnSendTransactionSuccess(successMessage),
    (errorMessage) => OnSendTransactionError(errorMessage));
```

6. Call Contract

- You need to use RPC Platform for Call Contract. In order to do this, create a new connector.

```
Platform myNewPlatform = UnityPlatform.New(PlatformConnectorType.RPC);
```

```
connector.CallContract(
    ERC20Standart.BalanceOf(callContractContractAddress, callOtherUserWalletAddress),
    (successMessage) => OnCallContractSuccess(successMessage),
    (errorMessage) => OnCallContractError(errorMessage));
```

- If you want to send a custom query, here is the code:

```
ContractInteraction myContractInteraction = new ContractInteraction(_ContractAdress, _EntryPoint, _CallData);
connector.CallContract(
       myContractInteraction,
       (successMessage) => OnCallContractSuccess(successMessage),
       (errorMessage) => OnCallContractError(errorMessage)
    );
```

### Contributors

Thanks to these amazing people for their contributions.

<table>
  <tbody>
    <tr>
      <td align="center" valign="top" width="25%"><a href="https://github.com/lastceri"><img src="https://avatars.githubusercontent.com/u/125711498?v=4" width="125px;" alt="lastceri"/><br/><b>lastceri</b></a><br/>Developer</td>
      <td align="center" valign="top" width="25%"><a href="https://github.com/sektor7k"><img src="https://avatars.githubusercontent.com/u/76495441?v=4" width="125px;" alt="sektor7k"/><br/><b>sektor7k</b></a><br/>Developer</td>
      <td align="center" valign="top" width="25%"><a href="https://github.com/cannsky"><img src="https://avatars.githubusercontent.com/u/44663880?v=4" width="125px;" alt="cannsky"/><br/><b>cannsky</b></a><br />Developer</td>
    </tr>
  </tbody>
</table>
