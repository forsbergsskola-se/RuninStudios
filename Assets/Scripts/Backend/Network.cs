using System;
using UnityEngine;

namespace Backend
{
    public class Network : MonoBehaviour
    {
        private static BrainCloudWrapper bc;

        public static Network sharedInstance;

        public delegate void AuthenticationRequestCompleted();
        public delegate void AuthenticationRequestFailed();
        

        private void Awake()
        {
            bc = FindObjectOfType<BrainCloudWrapper>();
            DontDestroyOnLoad(this);
            bc.Init();
        }

        private void Start()
        {
            RequestAnonymousAuthentication();
        }

        void Update()
        {
            bc.RunCallbacks();
        }

        public bool IsAuthenticated() { return bc.Client.Authenticated; } // To validate authentication

        public void RequestAnonymousAuthentication(AuthenticationRequestCompleted authenticationRequestCompleted = null,
            AuthenticationRequestFailed authenticationRequestFailed = null)
        {
            // Success callback lambda
            BrainCloud.SuccessCallback successCallback = (responseData, cbobject) =>
            {
                Debug.Log($"RequestAnonymousAuthentication success: {responseData}");
                if (authenticationRequestCompleted != null) { authenticationRequestCompleted(); }
            };
        
            // Failure callback lambda
            BrainCloud.FailureCallback failureCallback = (statusMessage, code, erreor, cbobject) =>
            {
                Debug.Log($"RequestAnonymousAuthentication failed: {statusMessage}");
                if (authenticationRequestFailed != null) { authenticationRequestFailed(); }
            };
        
            // BrainCloud request
            bc.AuthenticateAnonymous(successCallback, failureCallback);
        }
    }
}   