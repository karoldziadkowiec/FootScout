import React, { useEffect, useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { Table, Form, Button, Row, Col, Modal, FormSelect } from 'react-bootstrap';
import { ToastContainer, toast } from 'react-toastify';
import AccountService from '../../services/api/AccountService';
import UserService from '../../services/api/UserService';
import PlayerPositionService from '../../services/api/PlayerPositionService';
import PlayerFootService from '../../services/api/PlayerFootService';
import UserDTO from '../../models/dtos/UserDTO';
import ClubHistoryModel from '../../models/interfaces/ClubHistory';
import PlayerAdvertisement from '../../models/interfaces/PlayerAdvertisement';
import PlayerPosition from '../../models/interfaces/PlayerPosition';
import PlayerFoot from '../../models/interfaces/PlayerFoot';
import '../../App.css';
import '../../styles/user/MyPlayerAdvertisements.css';


const MyPlayerAdvertisements = () => {
    const navigate = useNavigate();
    const location = useLocation();
    const [user, setUser] = useState<UserDTO | null>(null);
    const [userClubHistories, setUserClubHistories] = useState<ClubHistoryModel[]>([]);
    const [userPlayerAdvertisements, setUserPlayerAdvertisements] = useState<PlayerAdvertisement[]>([]);
    const [positions, setPositions] = useState<PlayerPosition[]>([]);
    const [feet, setFeet] = useState<PlayerFoot[]>([]);
    const [showDetailsModal, setShowDetailsModal] = useState<boolean>(false);
    const [showClubHistoryDetailsModal, setShowClubHistoryDetailsModal] = useState<boolean>(false);
    const [selectedPlayerAdvertisement, setSelectedPlayerAdvertisement] = useState<PlayerAdvertisement | null>(null);
    const [selectedClubHistory, setSelectedClubHistory] = useState<ClubHistoryModel | null>(null);
    
    useEffect(() => {
        if (location.state && location.state.toastMessage)
            toast.success(location.state.toastMessage);

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

        fetchUserData();
        fetchPositions();
        fetchFeet();
    }, [location]);

    const getPositionNameById = (id: number) => {
        const position = positions.find(p => p.id === id);
        return position ? position.positionName : 'Unknown';
    };

    const getFootNameById = (id: number) => {
        const foot = feet.find(f => f.id === id);
        return foot ? foot.footName : 'Unknown';
    };

    const handleShowDetails = (playerAdvertisement: PlayerAdvertisement) => {
        setSelectedPlayerAdvertisement(playerAdvertisement);
        setShowDetailsModal(true);
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

    const calculateDaysLeft = (startDate: string, endDate: string): number => {
        const start = new Date(startDate);
        const end = new Date(endDate);
        const timeDiff = end.getTime() - start.getTime();
        const daysLeft = timeDiff / (1000 * 3600 * 24);
        return Math.ceil(daysLeft);
    };

    return (
        <div className="MyPlayerAdvertisements">
            <ToastContainer />
            <h1>My Player Advertisements</h1>
            <Button variant="success" onClick={() => navigate('/new-player-advertisement')}>
                <i className="bi bi-file-earmark-plus-fill"></i>
                New Player Advertisement
            </Button>
            <div className="table-responsive">
                <Table striped bordered hover>
                    <thead>
                        <tr>
                            <th>Creation Date (Days left)</th>
                            <th>Position</th>
                            <th>League</th>
                            <th>Region</th>
                            <th>Salary (zł.)</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {userPlayerAdvertisements.length > 0 ? (
                            userPlayerAdvertisements.map((advertisement, index) => (
                                <tr key={index}>
                                    <td>{formatDate(advertisement.creationDate)} ({calculateDaysLeft(advertisement.startDate, advertisement.endDate)} days)</td>
                                    <td>{getPositionNameById(advertisement.playerPositionId)}</td>
                                    <td>{advertisement.league}</td>
                                    <td>{advertisement.region}</td>
                                    <td>{advertisement.salaryRange.min} - {advertisement.salaryRange.max}</td>
                                    <td>
                                        <Button variant="dark" className="button-spacing" onClick={() => handleShowDetails(advertisement)}>
                                            <i className="bi bi-info-square"></i>
                                        </Button>
                                        <Button variant="warning" className="button-spacing">
                                            <i className="bi bi-pencil-square"></i>
                                        </Button>
                                        <Button variant="danger">
                                            <i className="bi bi-trash"></i>
                                        </Button>
                                    </td>
                                </tr>
                            ))
                        ) : (
                            <tr>
                                <td colSpan={9} className="text-center">No club player advertisement available</td>
                            </tr>
                        )}
                    </tbody>
                </Table>
            </div>

            {/* Details of Player Advertisement Modal */}
            <Modal size="lg" show={showDetailsModal} onHide={() => setShowDetailsModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Player Advertisement Details</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    {selectedPlayerAdvertisement && user && (
                        <div className="modal-content-centered">
                            <p><strong>Creation Date (Days left):</strong> {formatDate(selectedPlayerAdvertisement.creationDate)} ({calculateDaysLeft(selectedPlayerAdvertisement.startDate, selectedPlayerAdvertisement.endDate)} days) </p>
                            <p><strong>D A T A</strong></p>
                            <p><strong>Name:</strong> {user.firstName} {user.lastName} </p>
                            <p><strong>E-mail:</strong> {user.email} </p>
                            <p><strong>Phone number:</strong> {user.phoneNumber} </p>
                            <p><strong>Location:</strong> {user.location} </p>
                            <p><strong>P L A Y E R - P R O F I L E</strong></p>
                            <p><strong>Age:</strong> {selectedPlayerAdvertisement.age}</p>
                            <p><strong>Height:</strong> {selectedPlayerAdvertisement.height}</p>
                            <p><strong>Foot:</strong> {getFootNameById(selectedPlayerAdvertisement.playerFootId)} </p>
                            <p><strong>P R E F E R E N C E S</strong></p>
                            <p><strong>Position:</strong> {getPositionNameById(selectedPlayerAdvertisement.playerPositionId)} </p>
                            <p><strong>League (Region):</strong> {selectedPlayerAdvertisement.league} ({selectedPlayerAdvertisement.region}) </p>
                            <p><strong>Salary (zł.): </strong> {selectedPlayerAdvertisement.salaryRange.min} - {selectedPlayerAdvertisement.salaryRange.max} </p>
                            <p><strong>C L U B - H I S T O R Y</strong></p>
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
                                            <td colSpan={9} className="text-center">No club history available</td>
                                        </tr>
                                    )}
                                </tbody>
                            </Table>
                        </div>
                    )}
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setShowDetailsModal(false)}>Close</Button>
                </Modal.Footer>
            </Modal>

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

export default MyPlayerAdvertisements;