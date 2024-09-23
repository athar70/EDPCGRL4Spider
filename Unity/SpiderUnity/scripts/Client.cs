using NetMQ;
using NetMQ.Sockets;
using System;
using System.Collections.Concurrent;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Receiver
{
    private readonly Thread receiveThread;
    private bool running;

    public Receiver()
    {
        receiveThread = new Thread((object callback) =>
        {
            using (var socket = new PullSocket())
            {
                socket.Connect("tcp://localhost:5555");

                while (running)
                {
                    string message = socket.ReceiveFrameString();
                    Data data = JsonUtility.FromJson<Data>(message);
                    ((Action<Data>)callback)(data);
                }
            }
        });
    }

    // Starts the receiver thread
    public void Start(Action<Data> callback)
    {
        running = true;
        receiveThread.Start(callback);
    }

    // Stops the receiver thread
    public void Stop()
    {
        running = false;
        receiveThread.Join();
    }
}

public class Client : MonoBehaviour
{
    private readonly ConcurrentQueue<Action> runOnMainThread = new ConcurrentQueue<Action>();
    private Receiver receiver;

    private void Start()
    {
        DontDestroyOnLoad(gameObject); // Make this object persist between scenes

        receiver = new Receiver();
        receiver.Start((Data data) => runOnMainThread.Enqueue(() =>
        {
            Debug.Log(data.str);

            // Scene management based on received data
            HandleSceneChange(data.str);
            
            // Update attributes if in SpiderScene
            if (SceneManager.GetActiveScene().name.Equals("SpiderScene"))
            {
                SetAttributes(data);
            }
        }));
    }

    private void Update()
    {
        // Execute actions queued to run on the main thread
        while (runOnMainThread.TryDequeue(out Action action))
        {
            action.Invoke();
        }
    }

    private void OnApplicationQuit()
    {
        Debug.Log("Stopping Receiver on Application Quit...");
        receiver.Stop();
        NetMQConfig.Cleanup();  // Clean up NetMQ resources
    }

    // Handle scene changes based on received data
    private void HandleSceneChange(string sceneName)
    {
        if (sceneName.Equals("Relaxing") && !SceneManager.GetActiveScene().name.Equals("RelaxingScene"))
        {
            Debug.Log("Changing scene to RelaxingScene");
            SceneManager.LoadScene("RelaxingScene");
        }
        else if (sceneName.Equals("Stressful") && !SceneManager.GetActiveScene().name.Equals("SpiderScene"))
        {
            Debug.Log("Changing scene to SpiderScene");
            SceneManager.LoadScene("SpiderScene");
        }

        Debug.Log("Current Scene: " + SceneManager.GetActiveScene().name);
    }

    // Set attributes based on received data
    private void SetAttributes(Data data)
    {
        Debug.Log("Setting attributes...");
        setAttributes.instance.Locomotion = data.Locomotion;
        setAttributes.instance.Motion = data.Motion;
        setAttributes.instance.Closeness = data.Closeness;
        setAttributes.instance.Largness = data.Largness;
        setAttributes.instance.Hairiness = data.Hairiness;
        setAttributes.instance.Color = data.Color;
    }
}
