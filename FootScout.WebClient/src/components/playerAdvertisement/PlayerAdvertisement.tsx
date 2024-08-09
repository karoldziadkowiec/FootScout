import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { Table, Form, Button, Row, Col, Modal, FormSelect } from 'react-bootstrap';
import { ToastContainer, toast } from 'react-toastify';
import AccountService from '../../services/api/AccountService';
import UserService from '../../services/api/UserService';
import PlayerAdvertisementService from '../../services/api/PlayerAdvertisementService';
import PlayerAdvertisementFavoriteService from '../../services/api/PlayerAdvertisementFavoriteService';
import PlayerPositionService from '../../services/api/PlayerPositionService';
import PlayerFootService from '../../services/api/PlayerFootService';
import UserDTO from '../../models/dtos/UserDTO';
import ClubHistoryModel from '../../models/interfaces/ClubHistory';
import PlayerAdvertisementModel from '../../models/interfaces/PlayerAdvertisement';
import PlayerAdvertisementFavoriteCreateDTO from '../../models/dtos/PlayerAdvertisementFavoriteCreateDTO';
import PlayerPosition from '../../models/interfaces/PlayerPosition';
import PlayerFoot from '../../models/interfaces/PlayerFoot';
import '../../App.css';
import '../../styles/playerAdvertisement/PlayerAdvertisement.css';

const PlayerAdvertisement = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [userId, setUserId] = useState<string | null>(null);
    const [playerAdvertisement, setPlayerAdvertisement] = useState<PlayerAdvertisementModel | null>(null);
    const [player, setPlayer] = useState<UserDTO | null>(null);
    const [playerClubHistories, setPlayerClubHistories] = useState<ClubHistoryModel[]>([]);
    const [playerAdvertisementStatus, setPlayerAdvertisementStatus] = useState<boolean | null>(null);
    const [favoriteId, setFavoriteId] = useState<number>(0);
    const [positions, setPositions] = useState<PlayerPosition[]>([]);
    const [feet, setFeet] = useState<PlayerFoot[]>([]);
    const [isAdminRole, setIsAdminRole] = useState<boolean | null>(null);
    const [showClubHistoryDetailsModal, setShowClubHistoryDetailsModal] = useState<boolean>(false);
    const [showEditModal, setShowEditModal] = useState<boolean>(false);
    const [showDeleteModal, setShowDeleteModal] = useState<boolean>(false);
    const [showDeleteFavoriteModal, setShowDeleteFavoriteModal] = useState<boolean>(false);
    const [selectedClubHistory, setSelectedClubHistory] = useState<ClubHistoryModel | null>(null);
    const [editFormData, setEditFormData] = useState<PlayerAdvertisementModel | null>(null);
    const [favoritePlayerAdvertisementDTO, setFavoritePlayerAdvertisementDTO] = useState<PlayerAdvertisementFavoriteCreateDTO>({
        playerAdvertisementId: 0,
        userId: ''
    });
    const [deleteFavoriteId, setDeleteFavoriteId] = useState<number | null>(null);

    useEffect(() => {
        const fetchUserData = async () => {
            try {
                const userId = await AccountService.getId();
                if (userId)
                    setUserId(userId);

                const isAdmin = await AccountService.isRoleAdmin();
                setIsAdminRole(isAdmin);
            }
            catch (error) {
                console.error('Failed to fetch userId:', error);
                toast.error('Failed to load userId.');
            }
        };

        const fetchPlayerAdvertisementData = async (id: number) => {
            try {
                const playerAdvertisement = await PlayerAdvertisementService.getPlayerAdvertisement(id);
                setPlayerAdvertisement(playerAdvertisement);

                const playerData = await UserService.getUser(playerAdvertisement.userId);
                setPlayer(playerData);

                const playerClubHistories = await UserService.getUserClubHistory(playerAdvertisement.userId);
                setPlayerClubHistories(playerClubHistories);

                const endDate = new Date(playerAdvertisement.endDate);
                const currentDate = new Date();
                setPlayerAdvertisementStatus(endDate >= currentDate);

                if (userId) {
                    const favoriteId = await PlayerAdvertisementFavoriteService.checkPlayerAdvertisementIsFavorite(playerAdvertisement.id, userId);
                    setFavoriteId(favoriteId);
                }
            }
            catch (error) {
                console.error('Failed to fetch player advertisement:', error);
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

        if (id) {
            fetchUserData();
            fetchPlayerAdvertisementData(Number(id));
            fetchPositions();
            fetchFeet();
        }
    }, [id, userId]);

    if (!playerAdvertisement) {
        return <div><p><strong><h2>No advertisement found...</h2></strong></p></div>;
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

    const handleShowEditModal = (playerAdvertisement: PlayerAdvertisementModel) => {
        setEditFormData(playerAdvertisement);
        setShowEditModal(true);
    };

    const handleEditPlayerAdvertisement = async () => {
        if (!editFormData)
            return;

        const validationError = validateForm(editFormData);
        if (validationError) {
            toast.error(validationError);
            return;
        }

        const position = positions.find(pos => pos.id === editFormData.playerPositionId);
        if (!position) {
            toast.error('Invalid player position.');
            return;
        }

        const foot = feet.find(f => f.id === editFormData.playerFootId);
        if (!foot) {
            toast.error('Invalid foot name.');
            return;
        }

        try {
            const updatedFormData = {
                ...editFormData,
                playerPosition: position,
                playerFoot: foot
            };

            await PlayerAdvertisementService.updatePlayerAdvertisement(editFormData.id, updatedFormData);
            setShowEditModal(false);
            toast.success('Player advertisement updated successfully!');
            // Refresh the user data
            const _playerAdvertisement = await PlayerAdvertisementService.getPlayerAdvertisement(playerAdvertisement.id);
            setPlayerAdvertisement(_playerAdvertisement);
        } catch (error) {
            console.error('Failed to update player advertisement:', error);
            toast.error('Failed to update player advertisement.');
        }
    };

    const validateForm = (formData: PlayerAdvertisementModel) => {
        const { playerPositionId, league, region, age, height, playerFootId, salaryRange } = formData;
        const { min, max } = salaryRange;

        if (!playerPositionId || !league || !region || !age || !height || !playerFootId || !min || !max)
            return 'All fields are required.';

        if (isNaN(Number(age)) || isNaN(Number(height)) || isNaN(Number(min)) || isNaN(Number(max)))
            return 'Age, height, min and max salary must be numbers.';

        if (Number(age) < 0 || Number(height) < 0 || Number(min) < 0 || Number(max) < 0)
            return 'Age, height, min and max salary must be greater than or equal to 0.';

        if (max < min) {
            return 'Max Salary must be greater than Min Salary.';
        }

        return null;
    };

    const handleDeletePlayerAdvertisement = async () => {
        if (!playerAdvertisement)
            return;

        try {
            await PlayerAdvertisementService.deletePlayerAdvertisement(playerAdvertisement.id);
            navigate('/my-player-advertisements', { state: { toastMessage: "Your player advertisement has been deleted successfully." } });
        }
        catch (error) {
            console.error('Failed to delete user:', error);
            toast.error('Failed to delete user.');
        }
    };

    const handleAddToFavorite = async () => {
        if (!playerAdvertisement || !userId)
            return;

        try {
            const createFormData = { ...favoritePlayerAdvertisementDTO, playerAdvertisementId: playerAdvertisement.id, userId: userId };
            setFavoritePlayerAdvertisementDTO(createFormData);
            await PlayerAdvertisementFavoriteService.addToFavorites(createFormData);
            navigate('/my-favorite-player-advertisements', { state: { toastMessage: "Player advertisement has been added to favorites successfully." } });
        }
        catch (error) {
            console.error('Failed to delete user:', error);
            toast.error('Failed to delete user.');
        }
    };

    const handleShowDeleteFavoriteModal = (favoriteAdvertisementId: number) => {
        setDeleteFavoriteId(favoriteAdvertisementId);
        setShowDeleteFavoriteModal(true);
    };

    const handleDeleteFromFavorites = async () => {
        if (!userId || !deleteFavoriteId)
            return;

        try {
            await PlayerAdvertisementFavoriteService.deleteFromFavorites(deleteFavoriteId);
            toast.success('Your followed advertisement has been deleted from favorites successfully.');
            setShowDeleteFavoriteModal(false);
            setDeleteFavoriteId(null);
            // Refresh the user data
            const favoriteId = await PlayerAdvertisementFavoriteService.checkPlayerAdvertisementIsFavorite(playerAdvertisement.id, userId);
            setFavoriteId(favoriteId);
        }
        catch (error) {
            console.error('Failed to delete advertisement from favorites:', error);
            toast.error('Failed to delete advertisement from favorites.');
        }
    };

    const formatDate = (dateString: string): string => {
        const date = new Date(dateString);
        const day = String(date.getDate()).padStart(2, '0');
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const year = date.getFullYear();
        return `${day}-${month}-${year}`;
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
            <div className="ad-buttons-container mb-3">
                {playerAdvertisementStatus ? (
                    playerAdvertisement.userId === userId || isAdminRole ? (
                        <Row>
                            <Col>
                                <Button variant="warning" className="ad-form-button" onClick={() => handleShowEditModal(playerAdvertisement)}>
                                    <i className="bi bi-pencil-square"></i> Edit
                                </Button>
                            </Col>
                            <Col>
                                <Button variant="danger" className="ad-form-button" onClick={() => setShowDeleteModal(true)}>
                                    <i className="bi bi-trash"></i> Delete
                                </Button>
                            </Col>
                        </Row>
                    ) : (
                        <Row>
                            <Col>
                                <Button variant="primary" className="ad-form-button">
                                    <i className="bi bi-pen"></i> Propose contract
                                </Button>
                            </Col>
                            {favoriteId === 0 ? (
                                <Col>
                                    <Button variant="success" onClick={handleAddToFavorite}>
                                        <i className="bi bi-heart-fill"></i>
                                    </Button>
                                </Col>
                            ) : (
                                <Col>
                                    <Button variant="danger" onClick={() => handleShowDeleteFavoriteModal(favoriteId)}>
                                        <i className="bi bi-heart"></i>
                                    </Button>
                                </Col>
                            )}
                        </Row>
                    )
                ) : (
                    <div className="ad-status-container">
                        <p>Status: <strong>Inactive</strong></p>
                    </div>
                )}
            </div>
            <div className="ad-container">
                {playerAdvertisement && player && (
                    <div>
                        <p><Form.Label className="ad-name-label">{(player.firstName).toUpperCase()} {(player.lastName).toUpperCase()}</Form.Label></p>
                        <Row>
                            <Col>
                                <Form.Label className="ad-section">CONTACT INFORMATION</Form.Label>
                                <p><Form.Label>E-mail: </Form.Label>
                                    <Form.Label className="ad-data-label">{player.email}</Form.Label>
                                </p>
                                <p><Form.Label>Phone number: </Form.Label>
                                    <Form.Label className="ad-data-label">{player.phoneNumber}</Form.Label>
                                </p>
                                <p><Form.Label>Location: </Form.Label>
                                    <Form.Label className="ad-data-label">{player.location}</Form.Label>
                                </p>
                            </Col>
                            <Col>
                                <Form.Label className="ad-section">PLAYER PROFILE</Form.Label>
                                <p><Form.Label>Age: </Form.Label>
                                    <Form.Label className="ad-data-label">{playerAdvertisement.age}</Form.Label>
                                </p>
                                <p><Form.Label>Height: </Form.Label>
                                    <Form.Label className="ad-data-label">{playerAdvertisement.height}</Form.Label>
                                </p>
                                <p><Form.Label>Foot: </Form.Label>
                                    <Form.Label className="ad-data-label">{getFootNameById(playerAdvertisement.playerFootId)}</Form.Label>
                                </p>
                            </Col>
                            <Col>
                                <Form.Label className="ad-section">PREFERENCES</Form.Label>
                                <p><Form.Label>Position: </Form.Label>
                                    <Form.Label className="ad-data-label">{getPositionNameById(playerAdvertisement.playerPositionId)}</Form.Label>
                                </p>
                                <p><Form.Label>League (Region): </Form.Label>
                                    <Form.Label className="ad-data-label">{playerAdvertisement.league} ({playerAdvertisement.region})</Form.Label>
                                </p>
                                <p><Form.Label>Salary (zł.): </Form.Label>
                                    <Form.Label className="ad-data-label">{playerAdvertisement.salaryRange.min} - {playerAdvertisement.salaryRange.max}</Form.Label>
                                </p>
                            </Col>
                        </Row>
                        <Form.Label className="ad-section">CLUB HISTORY</Form.Label>
                        <div className="ad-table-responsive">
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
                                    {playerClubHistories.length > 0 ? (
                                        playerClubHistories.map((history, index) => (
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

                            <p><Form.Label>Creation Date (Days left): </Form.Label>
                                <Form.Label className="ad-creationDate-label">
                                    {formatDate(playerAdvertisement.creationDate)} ({calculateDaysLeft(playerAdvertisement.endDate)} days)
                                </Form.Label>
                            </p>
                        </div>
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

            {/* Edit Club History Modal */}
            {
                editFormData && (
                    <Modal show={showEditModal} onHide={() => setShowEditModal(false)}>
                        <Modal.Header closeButton>
                            <Modal.Title>Edit Club History</Modal.Title>
                        </Modal.Header>
                        <Modal.Body>
                            <Form>
                                <Form.Group className="mb-3" controlId="formPosition">
                                    <Form.Label>Position</Form.Label>
                                    <FormSelect
                                        value={editFormData.playerPositionId}
                                        onChange={(e) => setEditFormData({
                                            ...editFormData,
                                            playerPositionId: parseInt(e.target.value, 10)
                                        })}
                                    >
                                        <option value="">Select Position</option>
                                        {positions.map((position) => (
                                            <option key={position.id} value={position.id}>
                                                {position.positionName}
                                            </option>
                                        ))}
                                    </FormSelect>
                                </Form.Group>
                                <Row>
                                    <Col>
                                        <Form.Group className="mb-3" controlId="formLeague">
                                            <Form.Label>Preferred League</Form.Label>
                                            <Form.Control
                                                type="text"
                                                placeholder="League"
                                                value={editFormData.league}
                                                onChange={(e) => setEditFormData({ ...editFormData, league: e.target.value })}
                                                maxLength={30}
                                                required
                                            />
                                        </Form.Group>
                                    </Col>
                                    <Col>
                                        <Form.Group className="mb-3" controlId="formRegion">
                                            <Form.Label>Region</Form.Label>
                                            <Form.Control
                                                type="text"
                                                placeholder="Region"
                                                value={editFormData.region}
                                                onChange={(e) => setEditFormData({ ...editFormData, region: e.target.value })}
                                                maxLength={30}
                                                required
                                            />
                                        </Form.Group>
                                    </Col>
                                </Row>
                                <Row>
                                    <Col>
                                        <Form.Group className="mb-3" controlId="formAge">
                                            <Form.Label>Age</Form.Label>
                                            <Form.Control
                                                type="number"
                                                placeholder="Age"
                                                value={editFormData.age}
                                                onChange={(e) => setEditFormData({ ...editFormData, age: parseInt(e.target.value) })}
                                                required
                                            />
                                        </Form.Group>
                                    </Col>
                                    <Col>
                                        <Form.Group className="mb-3" controlId="formHeight">
                                            <Form.Label>Height (cm)</Form.Label>
                                            <Form.Control
                                                type="number"
                                                placeholder="Height"
                                                value={editFormData.height}
                                                onChange={(e) => setEditFormData({ ...editFormData, height: parseInt(e.target.value) })}
                                                required
                                            />
                                        </Form.Group>
                                    </Col>
                                </Row>
                                <Form.Group className="mb-3" controlId="formFoot">
                                    <Form.Label>Foot</Form.Label>
                                    <FormSelect
                                        value={editFormData.playerFootId}
                                        onChange={(e) => setEditFormData({
                                            ...editFormData,
                                            playerFootId: parseInt(e.target.value, 10)
                                        })}
                                    >
                                        <option value="">Select Foot</option>
                                        {feet.map((foot) => (
                                            <option key={foot.id} value={foot.id}>
                                                {foot.footName}
                                            </option>
                                        ))}
                                    </FormSelect>
                                </Form.Group>
                                <Row>
                                    <Col>
                                        <Form.Group className="mb-3" controlId="formMin">
                                            <Form.Label>Min Salary (zł.)</Form.Label>
                                            <Form.Control
                                                type="number"
                                                placeholder="Min"
                                                value={editFormData.salaryRange.min}
                                                onChange={(e) => setEditFormData({
                                                    ...editFormData,
                                                    salaryRange: {
                                                        ...editFormData.salaryRange,
                                                        min: parseInt(e.target.value)
                                                    }
                                                })}
                                                required
                                            />
                                        </Form.Group>
                                    </Col>
                                    <Col>
                                        <Form.Group className="mb-3" controlId="formMax">
                                            <Form.Label>Max Salary (zł.)</Form.Label>
                                            <Form.Control
                                                type="number"
                                                placeholder="Max"
                                                value={editFormData.salaryRange.max}
                                                onChange={(e) => setEditFormData({
                                                    ...editFormData,
                                                    salaryRange: {
                                                        ...editFormData.salaryRange,
                                                        max: parseInt(e.target.value)
                                                    }
                                                })}
                                                required
                                            />
                                        </Form.Group>
                                    </Col>
                                </Row>
                            </Form>
                        </Modal.Body>
                        <Modal.Footer>
                            <Button variant="secondary" onClick={() => setShowEditModal(false)}>Close</Button>
                            <Button variant="success" onClick={handleEditPlayerAdvertisement}>Update</Button>
                        </Modal.Footer>
                    </Modal>
                )
            }

            {/* Delete Player Advertisement */}
            <Modal show={showDeleteModal} onHide={() => setShowDeleteModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Confirm action</Modal.Title>
                </Modal.Header>
                <Modal.Body>Are you sure you want to delete your advertisement?</Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setShowDeleteModal(false)}>Cancel</Button>
                    <Button variant="danger" onClick={handleDeletePlayerAdvertisement}>Delete</Button>
                </Modal.Footer>
            </Modal>

            {/* Delete Favorite Player Advertisement Modal */}
            <Modal show={showDeleteFavoriteModal} onHide={() => setShowDeleteFavoriteModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Confirm action</Modal.Title>
                </Modal.Header>
                <Modal.Body>Are you sure you want to delete this advertisement from favorites?</Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setShowDeleteFavoriteModal(false)}>Cancel</Button>
                    <Button variant="danger" onClick={handleDeleteFromFavorites}>Delete</Button>
                </Modal.Footer>
            </Modal>
        </div >
    );
}

export default PlayerAdvertisement;