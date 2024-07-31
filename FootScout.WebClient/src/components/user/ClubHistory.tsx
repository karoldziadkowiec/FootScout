import React, { useEffect, useState } from 'react';
import { Table, Form, Button, Row, Col, Modal } from 'react-bootstrap';
import { ToastContainer, toast } from 'react-toastify';
import AccountService from '../../services/api/AccountService';
import UserService from '../../services/api/UserService';
import ClubHistoryService from '../../services/api/ClubHistoryService';
import ClubHistoryCreateDTO from '../../models/dtos/ClubHistoryCreateDTO';
import UserDTO from '../../models/dtos/UserDTO';
import '../../App.css';
import '../../styles/user/ClubHistory.css';

const ClubHistory: React.FC = () => {
    const [user, setUser] = useState<UserDTO | null>(null);
    const [clubHistories, setClubHistories] = useState<ClubHistoryCreateDTO[]>([]);
    const [showCreateModal, setShowCreateModal] = useState<boolean>(false);

    const [addFormData, setAddFormData] = useState<ClubHistoryCreateDTO>({
        position: '',
        clubName: '',
        league: '',
        region: '',
        startDate: '',
        endDate: '',
        achievements: {
            numberOfMatches: 0,
            goals: 0,
            assists: 0,
            additionalAchievements: '',
        },
        userId: ''
    });

    useEffect(() => {
        const fetchUserData = async () => {
            try {
                const userId = await AccountService.getId();
                if (userId) {
                    const userData = await UserService.getUser(userId);
                    setUser(userData);

                    const userClubHistories = await ClubHistoryService.getUserAllClubHistory(userId);
                    setClubHistories(userClubHistories);
                }
            } 
            catch (error) {
                console.error('Failed to fetch user data:', error);
                toast.error('Failed to load user data.');
            }
        };
        fetchUserData();
    }, []);

    const formatDate = (dateString: string): string => {
        const date = new Date(dateString);
        const day = String(date.getDate()).padStart(2, '0');
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const year = date.getFullYear();
        return `${day}-${month}-${year}`;
    };

    const handleCreateClubHistory = async () => {
        if(!user) 
            return;

        try {
            const newFormData = { ...addFormData, userId: user.id };

            await ClubHistoryService.createClubHistory(newFormData);
            setShowCreateModal(false);
            toast.success('Club history created successfully!');
            
            const userClubHistories = await ClubHistoryService.getUserAllClubHistory(user.id);
            setClubHistories(userClubHistories);
        } 
        catch (error) {
            console.error('Failed to create club history:', error);
            toast.error('Failed to create club history.');
        }
    };

    return (
        <div className="ClubHistory">
            <h1>Club History</h1>
            <Button variant="success" className="form-button"onClick={() => setShowCreateModal(true)}>Create Club History</Button>
            <Table striped bordered hover>
                <thead>
                    <tr>
                        <th>Position</th>
                        <th>Club</th>
                        <th>League</th>
                        <th>Region</th>
                        <th>Date</th>
                        <th>Matches</th>
                        <th>Goals</th>
                        <th>Assists</th>
                        <th>Achievements</th>
                    </tr>
                </thead>
                <tbody>
                    {clubHistories.length > 0 ? (
                        clubHistories.map((history, index) => (
                            <tr key={index}>
                                <td>{history.position}</td>
                                <td>{history.clubName}</td>
                                <td>{history.league}</td>
                                <td>{history.region}</td>
                                <td>{formatDate(history.startDate)} - {formatDate(history.endDate)}</td>
                                <td>{history.achievements.numberOfMatches || 'N/A'}</td>
                                <td>{history.achievements.goals || 'N/A'}</td>
                                <td>{history.achievements.assists || 'N/A'}</td>
                                <td>{history.achievements.additionalAchievements || 'N/A'}</td>
                            </tr>
                        ))
                    ) : (
                        <tr>
                            <td colSpan={9} className="text-center">No club history available</td>
                        </tr>
                    )}
                </tbody>
            </Table>

            <Modal show={showCreateModal} onHide={() => setShowCreateModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Create Club History</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <Form>
                        <Form.Group as={Row} controlId="formPosition">
                            <Form.Label column sm="3">Position</Form.Label>
                            <Col sm="9">
                                <Form.Control 
                                    type="text" 
                                    placeholder="Position" 
                                    value={addFormData.position} 
                                    onChange={(e) => setAddFormData({ ...addFormData, position: e.target.value })} 
                                />
                            </Col>
                        </Form.Group>

                        <Form.Group as={Row} controlId="formClubName">
                            <Form.Label column sm="3">Club Name</Form.Label>
                            <Col sm="9">
                                <Form.Control 
                                    type="text" 
                                    placeholder="Club Name" 
                                    value={addFormData.clubName} 
                                    onChange={(e) => setAddFormData({ ...addFormData, clubName: e.target.value })} 
                                />
                            </Col>
                        </Form.Group>

                        <Form.Group as={Row} controlId="formLeague">
                            <Form.Label column sm="3">League</Form.Label>
                            <Col sm="9">
                                <Form.Control 
                                    type="text" 
                                    placeholder="League" 
                                    value={addFormData.league} 
                                    onChange={(e) => setAddFormData({ ...addFormData, league: e.target.value })} 
                                />
                            </Col>
                        </Form.Group>

                        <Form.Group as={Row} controlId="formRegion">
                            <Form.Label column sm="3">Region</Form.Label>
                            <Col sm="9">
                                <Form.Control 
                                    type="text" 
                                    placeholder="Region" 
                                    value={addFormData.region} 
                                    onChange={(e) => setAddFormData({ ...addFormData, region: e.target.value })} 
                                />
                            </Col>
                        </Form.Group>

                        <Form.Group as={Row} controlId="formStartDate">
                            <Form.Label column sm="3">Start Date</Form.Label>
                            <Col sm="9">
                                <Form.Control 
                                    type="date" 
                                    value={addFormData.startDate} 
                                    onChange={(e) => setAddFormData({ ...addFormData, startDate: e.target.value })} 
                                />
                            </Col>
                        </Form.Group>

                        <Form.Group as={Row} controlId="formEndDate">
                            <Form.Label column sm="3">End Date</Form.Label>
                            <Col sm="9">
                                <Form.Control 
                                    type="date" 
                                    value={addFormData.endDate} 
                                    onChange={(e) => setAddFormData({ ...addFormData, endDate: e.target.value })} 
                                />
                            </Col>
                        </Form.Group>

                        <Form.Group as={Row} controlId="formAchievements">
                            <Form.Label column sm="3">Achievements</Form.Label>
                            <Col sm="9">
                                <Form.Control 
                                    type="number" 
                                    placeholder="Number of Matches" 
                                    value={addFormData.achievements.numberOfMatches} 
                                    onChange={(e) => setAddFormData({ 
                                        ...addFormData, 
                                        achievements: { 
                                            ...addFormData.achievements, 
                                            numberOfMatches: parseInt(e.target.value) 
                                        } 
                                    })} 
                                />
                                <Form.Control 
                                    type="number" 
                                    placeholder="Goals" 
                                    value={addFormData.achievements.goals} 
                                    onChange={(e) => setAddFormData({ 
                                        ...addFormData, 
                                        achievements: { 
                                            ...addFormData.achievements, 
                                            goals: parseInt(e.target.value) 
                                        } 
                                    })} 
                                />
                                <Form.Control 
                                    type="number" 
                                    placeholder="Assists" 
                                    value={addFormData.achievements.assists} 
                                    onChange={(e) => setAddFormData({ 
                                        ...addFormData, 
                                        achievements: { 
                                            ...addFormData.achievements, 
                                            assists: parseInt(e.target.value) 
                                        } 
                                    })} 
                                />
                                <Form.Control 
                                    type="text" 
                                    placeholder="Additional Achievements" 
                                    value={addFormData.achievements.additionalAchievements} 
                                    onChange={(e) => setAddFormData({ 
                                        ...addFormData, 
                                        achievements: { 
                                            ...addFormData.achievements, 
                                            additionalAchievements: e.target.value 
                                        } 
                                    })} 
                                />
                            </Col>
                        </Form.Group>
                    </Form>
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setShowCreateModal(false)}>Close</Button>
                    <Button variant="success" onClick={handleCreateClubHistory}>Create</Button>
                </Modal.Footer>
            </Modal>

            <ToastContainer />
        </div>
    );
}

export default ClubHistory;