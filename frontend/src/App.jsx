import { HubConnectionBuilder } from "@microsoft/signalr"
import { WaitingRoom } from "./Components/WaitingRoom";
import { Chat } from "./Components/Chat";
import { useState } from "react";

function App() {
  const [connection, setConnection] = useState(null);
  const [chatRoom, setChatRoom] = useState("");
  const [messages, setMessages] = useState([]);

  const joinChat = async (userName, chatRoom) => {
    var newConnection = new HubConnectionBuilder()
      .withUrl("http://localhost:5256/chat")
      .withAutomaticReconnect()
      .build();

    newConnection.on("ReceiveMessage", (userName, message) => {
      setMessages((messages) => [...messages, { userName, message }]);
    });
    try {
      await newConnection.start();
      await newConnection.invoke("JoinGroup", { userName, chatRoom });
      setChatRoom(chatRoom);
      setConnection(newConnection);
    }
    catch (error) {
      console.log(error);
    }
  };

  const sendMessage = async (message) => {
    await connection.invoke("SendMessage", message);
  }

  const closeChat = async () => {
    await connection.stop();
    setMessages([]);
    setConnection(null);
  }

  return (<div className="min-h-screen flex items-center justify-center bg-gray-950">
    {connection ? (<Chat chatRoom={chatRoom} messages={messages} sendMessage={sendMessage} closeChat={closeChat} />)
      : (<WaitingRoom joinChat={joinChat} />)}
  </div>)
}

export default App
