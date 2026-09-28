export const Message = ({ messageInfo }) => {
    return (
        <div className="w-fit">
            <span className="text-sm text-white">{messageInfo.userName}</span>
            <div className="p-2 bg-blue-500 rounded-lg shadow-md">
                {messageInfo.message}
            </div>
        </div>
    );
}