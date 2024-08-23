import React, { useEffect, useState } from 'react';
import { To, useLocation, useNavigate } from 'react-router-dom';
import { ToastContainer, toast } from 'react-toastify';
import { Card, Row, Col, Button } from 'react-bootstrap';
import UserService from '../../services/api/UserService';
import '../../App.css';
import '../../styles/admin/AdminDashboard.css';
import ClubHistoryService from '../../services/api/ClubHistoryService';
import ProblemService from '../../services/api/ProblemService';
import PlayerAdvertisementService from '../../services/api/PlayerAdvertisementService';
import ClubAdvertisementService from '../../services/api/ClubAdvertisementService';
import PlayerOfferService from '../../services/api/PlayerOfferService';
import ClubOfferService from '../../services/api/ClubOfferService';
import ChatService from '../../services/api/ChatService';

const AdminDashboard = () => {
    const location = useLocation();
    const navigate = useNavigate();
    const [userCount, setUserCount] = useState<number>(0);
    const [clubHistoryCount, setClubHistoryCount] = useState<number>(0);
    const [chatCount, setChatCount] = useState<number>(0);
    const [unsolvedReportedProblemCount, setUnsolvedReportedProblemCount] = useState<number>(0);
    const [playerAdvertisementCount, setPlayerAdvertisementCount] = useState<number>(0);
    const [clubAdvertisementCount, setClubAdvertisementCount] = useState<number>(0);
    const [playerOfferCount, setPlayerOfferCount] = useState<number>(0);
    const [clubOfferCount, setClubOfferCount] = useState<number>(0);

    useEffect(() => {
        if (location.state && location.state.toastMessage) {
            toast.success(location.state.toastMessage);
        }

        const fetchCountData = async () => {
            try {
                const _userCount = await UserService.getUserCount();
                setUserCount(_userCount);

                const _clubHistoryCount = await ClubHistoryService.getClubHistoryCount();
                setClubHistoryCount(_clubHistoryCount);

                const _chatCount = await ChatService.getChatCount();
                setChatCount(_chatCount);

                const _unsolvedReportedProblemCount = await ProblemService.getUnsolvedProblemCount();
                setUnsolvedReportedProblemCount(_unsolvedReportedProblemCount);

                const _playerAdvertisementCount = await PlayerAdvertisementService.getActivePlayerAdvertisementCount();
                setPlayerAdvertisementCount(_playerAdvertisementCount);

                const _clubAdvertisementCount = await ClubAdvertisementService.getActiveClubAdvertisementCount();
                setClubAdvertisementCount(_clubAdvertisementCount);

                const _playerOfferCount = await PlayerOfferService.getActivePlayerOfferCount();
                setPlayerOfferCount(_playerOfferCount);

                const _clubOfferCount = await ClubOfferService.getActiveClubOfferCount();
                setClubOfferCount(_clubOfferCount);
            }
            catch (error) {
                console.error('Failed to fetch count data:', error);
            }
        };

        fetchCountData();
    }, [location]);

    const refreshData = async () => {
        const _userCount = await UserService.getUserCount();
        setUserCount(_userCount);

        const _clubHistoryCount = await ClubHistoryService.getClubHistoryCount();
        setClubHistoryCount(_clubHistoryCount);

        const _chatCount = await ChatService.getChatCount();
        setChatCount(_chatCount);

        const _unsolvedReportedProblemCount = await ProblemService.getUnsolvedProblemCount();
        setUnsolvedReportedProblemCount(_unsolvedReportedProblemCount);

        const _playerAdvertisementCount = await PlayerAdvertisementService.getActivePlayerAdvertisementCount();
        setPlayerAdvertisementCount(_playerAdvertisementCount);

        const _clubAdvertisementCount = await ClubAdvertisementService.getActiveClubAdvertisementCount();
        setClubAdvertisementCount(_clubAdvertisementCount);

        const _playerOfferCount = await PlayerOfferService.getActivePlayerOfferCount();
        setPlayerOfferCount(_playerOfferCount);

        const _clubOfferCount = await ClubOfferService.getActiveClubOfferCount();
        setClubOfferCount(_clubOfferCount);
    };

    const handleCardClick = (route: To) => {
        navigate(route);
    };

    return (
        <div className="AdminDashboard">
            <ToastContainer />
            <h1><i className="bi bi-grid"></i> Dashboard</h1>
            <p></p>
            <Button variant="dark" onClick={refreshData}>
                <i className="bi bi-arrow-repeat"></i> Refresh
            </Button>
            <p></p>
            <Row>
                <Col md={3} className="mb-4">
                    <Card bg="primary" text="white" onClick={() => handleCardClick('/admin/users')} className="clickable-card">
                        <Card.Body>
                            <Card.Title><i className="bi bi-people-fill"></i></Card.Title>
                            <Card.Title>Users</Card.Title>
                            <Card.Text>
                                <h4>{userCount}</h4>
                            </Card.Text>
                        </Card.Body>
                    </Card>
                </Col>
                <Col md={3} className="mb-4">
                    <Card bg="secondary" text="white" onClick={() => handleCardClick('/admin/users')} className="clickable-card">
                        <Card.Body>
                            <Card.Title><i className="bi bi-clock-history"></i></Card.Title>
                            <Card.Title>Club History</Card.Title>
                            <Card.Text>
                                <h4>{clubHistoryCount}</h4>
                            </Card.Text>
                        </Card.Body>
                    </Card>
                </Col>
                <Col md={3} className="mb-4">
                    <Card bg="info" text="white" onClick={() => handleCardClick('/admin/users')} className="clickable-card">
                        <Card.Body>
                            <Card.Title><i className="bi bi-chat-text-fill"></i></Card.Title>
                            <Card.Title>Chats</Card.Title>
                            <Card.Text>
                                <h4>{chatCount}</h4>
                            </Card.Text>
                        </Card.Body>
                    </Card>
                </Col>
                <Col md={3} className="mb-4">
                    <Card bg="danger" text="white" onClick={() => handleCardClick('/admin/users')} className="clickable-card">
                        <Card.Body>
                            <Card.Title><i className="bi bi-cone-striped"></i></Card.Title>
                            <Card.Title>Reported Problems</Card.Title>
                            <Card.Text>
                                <h4>{unsolvedReportedProblemCount}</h4>
                            </Card.Text>
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
            <Row>
                <Col md={3} className="mb-4">
                    <Card bg="success" text="white" onClick={() => handleCardClick('/admin/users')} className="clickable-card">
                        <Card.Body>
                            <Card.Title><i className="bi bi-person-bounding-box"></i></Card.Title>
                            <Card.Title>Player Advertisement</Card.Title>
                            <Card.Text>
                                <h4>{playerAdvertisementCount}</h4>
                            </Card.Text>
                        </Card.Body>
                    </Card>
                </Col>
                <Col md={3} className="mb-4">
                    <Card bg="warning" text="white" onClick={() => handleCardClick('/admin/users')} className="clickable-card">
                        <Card.Body>
                            <Card.Title><i className="bi bi-shield-fill"></i></Card.Title>
                            <Card.Title>Club Advertisements</Card.Title>
                            <Card.Text>
                                <h4>{clubAdvertisementCount}</h4>
                            </Card.Text>
                        </Card.Body>
                    </Card>
                </Col>
                <Col md={3} className="mb-4">
                    <Card bg="dark" text="white" onClick={() => handleCardClick('/admin/users')} className="clickable-card">
                        <Card.Body>
                            <Card.Title><i className="bi bi-briefcase"></i></Card.Title>
                            <Card.Title>Player Offers</Card.Title>
                            <Card.Text>
                                <h4>{playerOfferCount}</h4>
                            </Card.Text>
                        </Card.Body>
                    </Card>
                </Col>
                <Col md={3} className="mb-4">
                    <Card bg="light" text="dark" onClick={() => handleCardClick('/admin/users')} className="clickable-card">
                        <Card.Body>
                            <Card.Title><i className="bi bi-briefcase-fill"></i></Card.Title>
                            <Card.Title>Club Offers</Card.Title>
                            <Card.Text>
                                <h4>{clubOfferCount}</h4>
                            </Card.Text>
                        </Card.Body>
                    </Card>
                </Col>
            </Row>
        </div>
    );
}

export default AdminDashboard;