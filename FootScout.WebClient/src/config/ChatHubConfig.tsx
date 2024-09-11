const ApiPORT: number = 7220;    // SQL Server
//const ApiPORT: number = 7104;    // PostgreSQL
//const ApiPORT: number = 7272;    // MongoDB

const HubName: string = 'chathub';

const ChatHubURL: string = `https://localhost:${ApiPORT}/${HubName}`;
export default ChatHubURL;