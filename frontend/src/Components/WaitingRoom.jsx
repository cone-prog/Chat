import { useState } from "react";

export const WaitingRoom = ({ joinChat }) => {
    const [userName, setUserName] = useState("");
    const [chatRoom, setChatRoom] = useState("");

    const onSubmit = (e) => {
        e.preventDefault();
        joinChat(userName, chatRoom);
    }

    return (
        <form
            onSubmit={onSubmit}
            className="border border-slate-700 bg-slate-900 max-w-lg w-full p-10 rounded-xl shadow-lg">
            <p className="mb-8 text-center font-mono bg-linear-to-r from-blue-300 to-cyan-400 bg-clip-text text-5xl font-extrabold text-transparent">
                Онлайн чат
            </p>

            <div className="mb-5">
                <p className="mb-2 text-sm font-medium text-slate-300">Имя пользователя</p>
                <input onChange={(e) => setUserName(e.target.value)} className="w-full rounded-lg border border-slate-700 bg-slate-800 px-4 py-3 text-white outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-500/30" />
            </div>

            <div className="mb-7">
                <p className="mb-2 text-sm font-medium text-slate-300">Название чата</p>
                <input onChange={(e) => setChatRoom(e.target.value)} className="w-full rounded-lg border border-slate-700 bg-slate-800 px-4 py-3 text-white outline-none focus:border-blue-500 focus:ring-2 focus:ring-blue-500/30" />
            </div>

            <button
                type="submit"
                className="w-full rounded-lg bg-blue-600 px-4 py-3 font-semibold text-white hover:bg-blue-500">
                Присоединиться
            </button>
        </form>
    );
};