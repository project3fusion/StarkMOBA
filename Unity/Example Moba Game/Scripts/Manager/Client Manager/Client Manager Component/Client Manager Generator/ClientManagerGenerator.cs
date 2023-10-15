using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientManagerGenerator
{
    private ClientManager clientManager;

    public ClientManagerObjectPoolGenerator clientManagerObjectPoolGenerator;

    public ClientManagerGenerator(ClientManager clientManager)
    {
        this.clientManager = clientManager;
        clientManagerObjectPoolGenerator = new ClientManagerObjectPoolGenerator(clientManager);
    }
}
