import React, { useEffect, useState } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';
import { Table, Form, Button, Row, Col, Modal, FormSelect } from 'react-bootstrap';
import { ToastContainer, toast } from 'react-toastify';
import AccountService from '../../services/api/AccountService';
import UserService from '../../services/api/UserService';
import PlayerAdvertisementService from '../../services/api/PlayerAdvertisementService';
import PlayerPositionService from '../../services/api/PlayerPositionService';
import PlayerFootService from '../../services/api/PlayerFootService';
import UserDTO from '../../models/dtos/UserDTO';
import ClubHistoryModel from '../../models/interfaces/ClubHistory';
import PlayerAdvertisementModel from '../../models/interfaces/PlayerAdvertisement';
import PlayerPosition from '../../models/interfaces/PlayerPosition';
import PlayerFoot from '../../models/interfaces/PlayerFoot';
import '../../App.css';
import '../../styles/playerAdvertisement/PlayerAdvertisement.css';

const PlayerAdvertisement = () => {
    const location = useLocation();
    const navigate = useNavigate();
    const [playerAdvertisement, setPlayerAdvertisement] = useState<PlayerAdvertisementModel | null>(null);
    const [playerAdvertisementStatus, setPlayerAdvertisementStatus] = useState<boolean | null>(null);
    const [user, setUser] = useState<UserDTO | null>(null);
    const [userClubHistories, setUserClubHistories] = useState<ClubHistoryModel[]>([]);
    const [userPlayerAdvertisements, setUserPlayerAdvertisements] = useState<PlayerAdvertisementModel[]>([]);
    const [positions, setPositions] = useState<PlayerPosition[]>([]);
    const [feet, setFeet] = useState<PlayerFoot[]>([]);
    const [showDetailsModal, setShowDetailsModal] = useState<boolean>(false);
    const [showClubHistoryDetailsModal, setShowClubHistoryDetailsModal] = useState<boolean>(false);
    const [selectedClubHistory, setSelectedClubHistory] = useState<ClubHistoryModel | null>(null);

    useEffect(() => {
        if (location.state && location.state.playerAdvertisementId) {
            const id = location.state.playerAdvertisementId;

            const fetchPlayerAdvertisement = async (id: number) => {
                try {
                    const data = await PlayerAdvertisementService.getPlayerAdvertisement(id);
                    setPlayerAdvertisement(data);

                    const endDate = new Date(data.endDate);
                    const currentDate = new Date();
                    if (endDate >= currentDate)
                        setPlayerAdvertisementStatus(true);
                    else
                        setPlayerAdvertisementStatus(false);
                }
                catch (error) {
                    console.error('Failed to fetch player advertisement:', error);
                }
            };

            const fetchUserData = async () => {
                try {
                    const userId = await AccountService.getId();
                    if (userId) {
                        const userData = await UserService.getUser(userId);
                        setUser(userData);

                        const _userClubHistories = await UserService.getUserClubHistory(userId);
                        setUserClubHistories(_userClubHistories);

                        const _userPlayerAdvertisements = await UserService.getUserPlayerAdvertisements(userId);
                        setUserPlayerAdvertisements(_userPlayerAdvertisements);
                    }
                }
                catch (error) {
                    console.error('Failed to fetch userId:', error);
                    toast.error('Failed to load userId.');
                }
            };

            const fetchPositions = async () => {
                try {
                    const positionsData = await PlayerPositionService.getPlayerPositions();
                    setPositions(positionsData);
                }
                catch (error) {
                    console.error('Failed to fetch positions:', error);
                    toast.error('Failed to load positions.');
                }
            };

            const fetchFeet = async () => {
                try {
                    const feetData = await PlayerFootService.getPlayerFeet();
                    setFeet(feetData);
                }
                catch (error) {
                    console.error('Failed to fetch foot names:', error);
                    toast.error('Failed to load foot names.');
                }
            };

            fetchPlayerAdvertisement(id);
            fetchUserData();
            fetchPositions();
            fetchFeet();
        }
    }, [location.state]);

    if (!playerAdvertisement) {
        return <div>Loading...</div>;
    }

    const getPositionNameById = (id: number) => {
        const position = positions.find(p => p.id === id);
        return position ? position.positionName : 'Unknown';
    };

    const getFootNameById = (id: number) => {
        const foot = feet.find(f => f.id === id);
        return foot ? foot.footName : 'Unknown';
    };

    const handleShowClubHistoryDetails = (clubHistory: ClubHistoryModel) => {
        setSelectedClubHistory(clubHistory);
        setShowClubHistoryDetailsModal(true);
    };

    const formatDate = (dateString: string): string => {
        const date = new Date(dateString);
        const day = String(date.getDate()).padStart(2, '0');
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const year = date.getFullYear();
        return `${day}-${month}-${year}`;
    };

    const formatDateToForm = (dateString: string): string => {
        const date = new Date(dateString);
        const day = String(date.getDate()).padStart(2, '0');
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const year = date.getFullYear();
        return `${year}-${month}-${day}`;
    };

    const calculateDaysLeft = (endDate: string): number => {
        const currentDate = new Date();
        const end = new Date(endDate);
        const timeDiff = end.getTime() - currentDate.getTime();
        const daysLeft = timeDiff / (1000 * 3600 * 24);
        return Math.ceil(daysLeft);
    };

    return (
        <div className="PlayerAdvertisement">
            <ToastContainer />
            <h1>Player Advertisement</h1>
            <div className="buttons-container mb-3">
                {playerAdvertisementStatus === true ? (
                    <Row>
                        <Col>
                            <Button variant="success" className="form-button">
                                <i className="bi bi-pencil-square"></i>
                                Edit
                            </Button>
                        </Col>
                        <Col>
                            <Button variant="danger" className="form-button">
                                <i className="bi bi-trash"></i>
                                Delete
                            </Button>
                        </Col>
                    </Row>
                ) : (
                    <p>Status: <strong>Inactive</strong></p>
                )}
            </div>
            <div className="ad-container">
                {playerAdvertisement && user && (
                    <div>
                        <p><Form.Label>Creation Date (Days left): </Form.Label>
                            <Form.Label className="green-label">
                                {formatDate(playerAdvertisement.creationDate)} ({calculateDaysLeft(playerAdvertisement.endDate)} days)
                            </Form.Label>
                        </p>

                        <p><Form.Label>CONTACT INFORMATION</Form.Label></p>
                        <p><Form.Label>Name: </Form.Label>
                            <Form.Label className="ad-green-label">{user.firstName} {user.lastName}</Form.Label>
                        </p>
                        <p><Form.Label>E-mail: </Form.Label>
                            <Form.Label className="ad-green-label">{user.email}</Form.Label>
                        </p>
                        <p><Form.Label>Phone number: </Form.Label>
                            <Form.Label className="ad-green-label">{user.phoneNumber}</Form.Label>
                        </p>
                        <p><Form.Label>Location: </Form.Label>
                            <Form.Label className="ad-green-label">{user.location}</Form.Label>
                        </p>

                        <p><Form.Label>PLAYER PROFILE</Form.Label></p>
                        <p><Form.Label>Age: </Form.Label>
                            <Form.Label className="ad-green-label">{playerAdvertisement.age}</Form.Label>
                        </p>
                        <p><Form.Label>Height: </Form.Label>
                            <Form.Label className="ad-green-label">{playerAdvertisement.height}</Form.Label>
                        </p>
                        <p><Form.Label>Foot: </Form.Label>
                            <Form.Label className="ad-green-label">{getFootNameById(playerAdvertisement.playerFootId)}</Form.Label>
                        </p>

                        <p><Form.Label>PREFERENCES</Form.Label></p>
                        <p><Form.Label>Position: </Form.Label>
                            <Form.Label className="ad-green-label">{getPositionNameById(playerAdvertisement.playerPositionId)}</Form.Label>
                        </p>
                        <p><Form.Label>League (Region): </Form.Label>
                            <Form.Label className="ad-green-label">{playerAdvertisement.league} ({playerAdvertisement.region})</Form.Label>
                        </p>
                        <p><Form.Label>Salary (zł.): </Form.Label>
                            <Form.Label className="ad-green-label">{playerAdvertisement.salaryRange.min} - {playerAdvertisement.salaryRange.max}</Form.Label>
                        </p>

                        <p><Form.Label>Club History</Form.Label></p>
                        <Table striped bordered hover>
                            <thead>
                                <tr>
                                    <th>Date</th>
                                    <th>Club</th>
                                    <th>League (Region)</th>
                                    <th>Position</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody>
                                {userClubHistories.length > 0 ? (
                                    userClubHistories.map((history, index) => (
                                        <tr key={index}>
                                            <td>{formatDate(history.startDate)} - {formatDate(history.endDate)}</td>
                                            <td>{history.clubName}</td>
                                            <td>{history.league} ({history.region})</td>
                                            <td>{getPositionNameById(history.playerPositionId)}</td>
                                            <td>
                                                <Button variant="dark" className="button-spacing" onClick={() => handleShowClubHistoryDetails(history)}>
                                                    <i className="bi bi-info-square"></i>
                                                </Button>
                                            </td>
                                        </tr>
                                    ))
                                ) : (
                                    <tr>
                                        <td colSpan={5} className="text-center">No club history available</td>
                                    </tr>
                                )}
                            </tbody>
                        </Table>
                    </div>
                )}
            </div>

            {/* Details of Club History Modal */}
            <Modal show={showClubHistoryDetailsModal} onHide={() => setShowClubHistoryDetailsModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Club History Details</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    {selectedClubHistory && (
                        <div className="modal-content-centered">
                            <p><strong>Club Name:</strong> {selectedClubHistory.clubName}</p>
                            <p><strong>League:</strong> {selectedClubHistory.league}</p>
                            <p><strong>Region:</strong> {selectedClubHistory.region}</p>
                            <p><strong>Start Date:</strong> {formatDate(selectedClubHistory.startDate)}</p>
                            <p><strong>End Date:</strong> {formatDate(selectedClubHistory.endDate)}</p>
                            <p><strong>Position:</strong> {getPositionNameById(selectedClubHistory.playerPositionId)}</p>
                            <p><strong>Matches:</strong> {selectedClubHistory.achievements.numberOfMatches}</p>
                            <p><strong>Goals:</strong> {selectedClubHistory.achievements.goals}</p>
                            <p><strong>Assists:</strong> {selectedClubHistory.achievements.assists}</p>
                            <p><strong>Additional Achievements:</strong> {selectedClubHistory.achievements.additionalAchievements}</p>
                        </div>
                    )}
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setShowClubHistoryDetailsModal(false)}>Close</Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
}

export default PlayerAdvertisement;