using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BackendManager : MonoBehaviour
{
    private static BrainCloudWrapper bc = null;

    public static BackendManager sharedInstance;

    public delegate void AuthenticationRequestCompleted();
    public delegate void AuthenticationRequestFailed();
    

    private void Awake()
    {
        DontDestroyOnLoad(this);
        bc.Init();
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