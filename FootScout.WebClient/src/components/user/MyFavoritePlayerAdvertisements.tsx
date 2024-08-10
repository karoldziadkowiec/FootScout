import React, { useEffect, useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { Table, Button, Modal } from 'react-bootstrap';
import { ToastContainer, toast } from 'react-toastify';
import AccountService from '../../services/api/AccountService';
import UserService from '../../services/api/UserService';
import PlayerPositionService from '../../services/api/PlayerPositionService';
import PlayerAdvertisementFavoriteService from '../../services/api/PlayerAdvertisementFavoriteService';
import PlayerAdvertisementFavorite from '../../models/interfaces/PlayerAdvertisementFavorite';
import PlayerPosition from '../../models/interfaces/PlayerPosition';
import '../../App.css';
import '../../styles/user/MyPlayerFavoriteAdvertisements.css';

const MyFavoritePlayerAdvertisements = () => {
    const navigate = useNavigate();
    const location = useLocation();
    const [userId, setUserId] = useState<string | null>();
    const [userActiveFavoritePlayerAdvertisements, setUserActiveFavoritePlayerAdvertisements] = useState<PlayerAdvertisementFavorite[]>([]);
    const [userInactiveFavoritePlayerAdvertisements, setUserInactiveFavoritePlayerAdvertisements] = useState<PlayerAdvertisementFavorite[]>([]);
    const [positions, setPositions] = useState<PlayerPosition[]>([]);
    const [showDeleteModal, setShowDeleteModal] = useState<boolean>(false);
    const [deleteFavoriteId, setDeleteFavoriteId] = useState<number | null>(null);

    useEffect(() => {
        if (location.state && location.state.toastMessage)
            toast.success(location.state.toastMessage);

        const fetchUserData = async () => {
            try {
                const userId = await AccountService.getId();
                if (userId) {
                    setUserId(userId);
                    const _userActiveFavoritePlayerAdvertisements = await UserService.getUserActivePlayerAdvertisementFavorites(userId);
                    setUserActiveFavoritePlayerAdvertisements(_userActiveFavoritePlayerAdvertisements);

                    const _userInactiveFavoritePlayerAdvertisements = await UserService.getUserInactivePlayerAdvertisementFavorites(userId);
                    setUserInactiveFavoritePlayerAdvertisements(_userInactiveFavoritePlayerAdvertisements);
                }
            }
            catch (error) {
                console.error('Failed to fetch user\'s data:', error);
                toast.error('Failed to load user\'s data.');
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

        fetchUserData();
        fetchPositions();
    }, [location]);

    const getPositionNameById = (id: number) => {
        const position = positions.find(p => p.id === id);
        return position ? position.positionName : 'Unknown';
    };

    const moveToPlayerAdvertisementPage = (playerAdvertisementId: number) => {
        navigate(`/player-advertisement/${playerAdvertisementId}`, { state: { playerAdvertisementId } });
    };

    const handleShowDeleteModal = (favoriteAdvertisementId: number) => {
        setDeleteFavoriteId(favoriteAdvertisementId);
        setShowDeleteModal(true);
    };

    const handleDeleteFromFavorites = async () => {
        if (!userId || !deleteFavoriteId)
            return;

        try {
            await PlayerAdvertisementFavoriteService.deleteFromFavorites(deleteFavoriteId);
            toast.success('Your followed advertisement has been deleted from favorites successfully.');
            setShowDeleteModal(false);
            setDeleteFavoriteId(null);
            // Refresh the user data
            const _userActiveFavoritePlayerAdvertisements = await UserService.getUserActivePlayerAdvertisementFavorites(userId);
            setUserActiveFavoritePlayerAdvertisements(_userActiveFavoritePlayerAdvertisements);

            const _userInactiveFavoritePlayerAdvertisements = await UserService.getUserInactivePlayerAdvertisementFavorites(userId);
            setUserInactiveFavoritePlayerAdvertisements(_userInactiveFavoritePlayerAdvertisements);
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
        <div className="MyPlayerFavoriteAdvertisements">
            <ToastContainer />
            <h1>My Favorite Player Advertisements</h1>
            <div className="table-responsive">
                {/* Active favorite advertisements*/}
                <h3>Active advertisements</h3>
                <Table striped bordered hover>
                    <thead>
                        <tr>
                            <th>Creation Date (Days left)</th>
                            <th>Name</th>
                            <th>Position</th>
                            <th>League (Region)</th>
                            <th>Salary (zł.)</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {userActiveFavoritePlayerAdvertisements.length > 0 ? (
                            userActiveFavoritePlayerAdvertisements.map((favoriteAdvertisement, index) => (
                                <tr key={index}>
                                    <td>{formatDate(favoriteAdvertisement.playerAdvertisement.creationDate)} ({calculateDaysLeft(favoriteAdvertisement.playerAdvertisement.endDate)} days)</td>
                                    <td>{favoriteAdvertisement.playerAdvertisement.user.firstName} {favoriteAdvertisement.playerAdvertisement.user.lastName}</td>
                                    <td>{getPositionNameById(favoriteAdvertisement.playerAdvertisement.playerPositionId)}</td>
                                    <td>{favoriteAdvertisement.playerAdvertisement.league} ({favoriteAdvertisement.playerAdvertisement.region})</td>
                                    <td>{favoriteAdvertisement.playerAdvertisement.salaryRange.min} - {favoriteAdvertisement.playerAdvertisement.salaryRange.max}</td>
                                    <td>
                                        <Button variant="dark" className="button-spacing" onClick={() => moveToPlayerAdvertisementPage(favoriteAdvertisement.playerAdvertisement.id)}>
                                            <i className="bi bi-info-square"></i>
                                        </Button>
                                        <Button variant="danger" onClick={() => handleShowDeleteModal(favoriteAdvertisement.id)}>
                                            <i className="bi bi-heart"></i>
                                        </Button>
                                    </td>
                                </tr>
                            ))
                        ) : (
                            <tr>
                                <td colSpan={9} className="text-center">No favorite player advertisement available</td>
                            </tr>
                        )}
                    </tbody>
                </Table>

                {/* Inactive favorite advertisements*/}
                <h3>Inactive advertisements</h3>
                <Table striped bordered hover>
                    <thead>
                        <tr>
                            <th>Creation Date (Days left)</th>
                            <th>Name</th>
                            <th>Position</th>
                            <th>League (Region)</th>
                            <th>Salary (zł.)</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {userInactiveFavoritePlayerAdvertisements.length > 0 ? (
                            userInactiveFavoritePlayerAdvertisements.map((favoriteAdvertisement, index) => (
                                <tr key={index}>
                                    <td>{formatDate(favoriteAdvertisement.playerAdvertisement.creationDate)} ({calculateDaysLeft(favoriteAdvertisement.playerAdvertisement.endDate)} days)</td>
                                    <td>{favoriteAdvertisement.playerAdvertisement.user.firstName} {favoriteAdvertisement.playerAdvertisement.user.lastName}</td>
                                    <td>{getPositionNameById(favoriteAdvertisement.playerAdvertisement.playerPositionId)}</td>
                                    <td>{favoriteAdvertisement.playerAdvertisement.league} ({favoriteAdvertisement.playerAdvertisement.region})</td>
                                    <td>{favoriteAdvertisement.playerAdvertisement.salaryRange.min} - {favoriteAdvertisement.playerAdvertisement.salaryRange.max}</td>
                                    <td>
                                        <Button variant="dark" className="button-spacing" onClick={() => moveToPlayerAdvertisementPage(favoriteAdvertisement.playerAdvertisement.id)}>
                                            <i className="bi bi-info-square"></i>
                                        </Button>
                                        <Button variant="danger" onClick={() => handleShowDeleteModal(favoriteAdvertisement.id)}>
                                            <i className="bi bi-heart"></i>
                                        </Button>
                                    </td>
                                </tr>
                            ))
                        ) : (
                            <tr>
                                <td colSpan={9} className="text-center">No favorite player advertisement available</td>
                            </tr>
                        )}
                    </tbody>
                </Table>
            </div>

            {/* Delete Favorite Player Advertisement Modal */}
            <Modal show={showDeleteModal} onHide={() => setShowDeleteModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Confirm action</Modal.Title>
                </Modal.Header>
                <Modal.Body>Are you sure you want to delete this advertisement from favorites?</Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setShowDeleteModal(false)}>Cancel</Button>
                    <Button variant="danger" onClick={handleDeleteFromFavorites}>Delete</Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
}

export default MyFavoritePlayerAdvertisements;