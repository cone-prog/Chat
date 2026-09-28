import { Message } from "./Message";
import { useState } from "react";

export const Chat = ({ chatRoom, messages, closeChat, sendMessage }) => {
    const [message, setMessage] = useState("");

    const onSendMessage = () => {
        sendMessage(message);
        setMessage("");
    }

    return (
        <div className="w-1/3 bg-slate-900 p-8 rounded shadow-lg">
            <div className="flex flex-row mb-5 justify-between">
                <p className="mb-8 font-mono bg-linear-to-r from-blue-300 to-cyan-400 bg-clip-text text-2xl font-extrabold text-transparent">{chatRoom}</p>
                <button className="border border-gray-400 text-white rounded w-8 h-8 flex items-center justify-center" onClick={closeChat}>×</button>

            </div>

            <div className="flex flex-col overflow-auto scroll-smooth h-96 gap-4 pb-3 text-white">
                {
                    messages.map((messageInfo, index) => (
                        <Message messageInfo={messageInfo} key={index}></Message>
                    ))
                }
            </div>

            <div className="flex flex-row m-4 gap-4 text-white">
                <input
                    value={message}
                    onChange={(e) => setMessage(e.target.value)}
                    className="flex-[3] text-lg border-4 border-blue-500 rounded-lg"
                    type="text"
                    placeholder="......." />
                <button onClick={onSendMessage} className="flex-1 bg-blue-500 rounded-full">Отправить</button>
            </div>
        </div>
    );
}