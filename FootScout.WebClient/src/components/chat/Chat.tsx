import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { Container, Row, Col, Form, Button, Card } from 'react-bootstrap';
import { ToastContainer, toast } from 'react-toastify';
import * as signalR from '@microsoft/signalr';
import ChatHubURL from '../../config/ChatHubConfig';
import TimeService from '../../services/time/TimeService';
import AccountService from '../../services/api/AccountService';
import ChatService from '../../services/api/ChatService';
import MessageService from '../../services/api/MessageService';
import ChatModel from '../../models/interfaces/Chat';
import Message from '../../models/interfaces/Message';
import UserDTO from '../../models/dtos/UserDTO';
import MessageSendDTO from '../../models/dtos/MessageSendDTO';
import '../../App.css';
import '../../styles/chat/Chat.css';

const Chat = () => {
    const { id } = useParams();
    const [userId, setUserId] = useState<string | null>(null);
    const [chatData, setChatData] = useState<ChatModel | null>(null);
    const [user, setUser] = useState<UserDTO | null>(null);
    const [receiver, setReceiver] = useState<UserDTO | null>(null);
    const [messages, setMessages] = useState<Message[]>([]);
    const [newMessage, setNewMessage] = useState<string>('');
    const [hubConnection, setHubConnection] = useState<signalR.HubConnection | null>(null);

    useEffect(() => {
        const fetchUserData = async () => {
            try {
                const userId = await AccountService.getId();
                if (userId) setUserId(userId);
            } catch (error) {
                console.error('Failed to fetch userId:', error);
                toast.error('Failed to load userId.');
            }
        };

        const fetchChatData = async (id: number) => {
            try {
                const _chatData = await ChatService.getChatById(id);
                setChatData(_chatData);

                if (_chatData.user1Id === userId) {
                    setUser(_chatData.user1);
                    setReceiver(_chatData.user2);
                } else {
                    setUser(_chatData.user2);
                    setReceiver(_chatData.user1);
                }

                const _messages = await MessageService.getMessagesForChat(id);
                setMessages(_messages);
            } catch (error) {
                console.error('Failed to fetch chat data:', error);
                toast.error('Failed to load chat data.');
            }
        };

        if (id) {
            fetchUserData();
            fetchChatData(Number(id));
        }
    }, [id, userId]);

    useEffect(() => {
        if (userId && id) {
            const connection = new signalR.HubConnectionBuilder()
                .withUrl(ChatHubURL)
                .withAutomaticReconnect()
                .build();

            connection.start()
                .then(() => {
                    console.log("Joined the chat.");
                    toast.success('Joined the chat.');
                    return connection.invoke("JoinChat", Number(id));
                })
                .catch(error => {
                    console.error('Failed to connect to SignalR hub:', error);
                    toast.error('Failed to connect to chat.');
                });

            connection.on("ReceiveMessage", (message: Message) => {
                setMessages(prevMessages => [...prevMessages, message]);
            });

            setHubConnection(connection);

            return () => {
                if (connection) {
                    connection.invoke("LeaveChat", Number(id))
                        .catch(error => console.error('Failed to leave chat:', error));
                    connection.stop()
                        .catch(error => console.error('Failed to stop connection:', error));
                }
            };
        }
    }, [userId, id]);

    const handleSendMessage = async () => {
        if (hubConnection && newMessage.trim() !== '') {
            try {
                if (chatData?.id && user?.id && receiver?.id) {
                    const messageSendDTO: MessageSendDTO = {
                        chatId: chatData.id,
                        senderId: user.id,
                        receiverId: receiver.id,
                        content: newMessage
                    };

                    await hubConnection.invoke("SendMessage", messageSendDTO);
                    setNewMessage('');

                    const _messages = await MessageService.getMessagesForChat(chatData.id);
                    setMessages(_messages);
                } else {
                    toast.error('Unable to send message. Missing required data.');
                }
            } catch (error) {
                console.error('Failed to send message:', error);
                toast.error('Failed to send message.');
            }
        }
    };

    if (!chatData || !(chatData.user1Id === userId || chatData.user2Id === userId)) {
        return <div><p><strong><h2>No chat found...</h2></strong></p></div>;
    }

    return (
        <Container className="Chat">
            <ToastContainer />
            <h1>Chat</h1>
            <div className="chat-container">
                <h2>{receiver?.firstName + ' ' + receiver?.lastName}</h2>
                <div className="messages">
                    {messages.map((message, index) => (
                        <Row key={index} className="my-2">
                            <Col xs={message.senderId === userId ? { span: 7, offset: 5 } : 7}>
                                <Row className="d-flex justify-content-between align-items-center">
                                    <Col xs="auto">
                                        <div className="message-timestamp">
                                            {TimeService.formatDateToEURWithHour(message.timestamp)}
                                        </div>
                                    </Col>
                                    <Col xs="auto">
                                        <Button variant="secondary" size='sm'>
                                            <i className="bi bi-trash"></i>
                                        </Button>
                                    </Col>
                                </Row>
                                <Card className={message.senderId === userId ? 'bg-primary text-white' : 'bg-light'}>
                                    <Card.Body>
                                        <Card.Text>{message.content}</Card.Text>
                                    </Card.Body>
                                </Card>
                            </Col>
                        </Row>
                    ))}
                </div>

                <Form className="message-input mt-3">
                    <Form.Group as={Row}>
                        <Col xs={10}>
                            <Form.Control
                                type="text"
                                value={newMessage}
                                onChange={(e) => setNewMessage(e.target.value)}
                                placeholder="Type a message"
                            />
                        </Col>
                        <Col xs={2}>
                            <Button variant="dark" onClick={handleSendMessage} className="w-100">
                                <i className="bi bi-send-fill"></i>
                            </Button>
                        </Col>
                    </Form.Group>
                </Form>
            </div>
        </Container>
    );
}

export default Chat;