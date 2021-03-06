using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using SocketConnection.NetworkRelays;

public class NetworkPositionRelay : NetworkRelay
    {
        public class NetworkPositionData : NetworkData {
            private float[] position;
            private string identifier;
            public NetworkPositionData(string identifier, Vector3 position) {
                this.position = new float[] { position.x, position.z};
                this.identifier = identifier;
                Debug.Log("NetworkPositionRelay: " + this.identifier);
                Debug.Log(this.position);
                Debug.Log(JsonConvert.SerializeObject(this));
            }
        }
        
        [Header("For objects with position")]
        public string identifier;
        public string className;

        private string generateIdentifier() {
            return $"{className}-{Guid.NewGuid()}";
        }


        public NetworkPositionData RelayData {
            get { return new NetworkPositionData(identifier, gameObject.transform.position); }
        }
        
        private void Start() {
            if (!ServerManager.Instance) return;
            ServerManager.Instance.CreateRelay(this);
            ServerManager.Instance.AddToRelaySet(this);
        }
        
        private void Awake() {
            if (string.IsNullOrEmpty(identifier)) {
                identifier = generateIdentifier();
            }
        }
    }
