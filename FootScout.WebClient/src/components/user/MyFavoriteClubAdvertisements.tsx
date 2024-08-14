import React, { useEffect, useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { Table, Button, Modal } from 'react-bootstrap';
import { ToastContainer, toast } from 'react-toastify';
import AccountService from '../../services/api/AccountService';
import UserService from '../../services/api/UserService';
import FavoriteClubAdvertisementService from '../../services/api/FavoriteClubAdvertisementService';
import FavoriteClubAdvertisement from '../../models/interfaces/FavoriteClubAdvertisement';
import '../../App.css';
import '../../styles/user/MyFavoriteClubAdvertisements.css';

const MyFavoriteClubAdvertisements = () => {
    const navigate = useNavigate();
    const location = useLocation();
    const [userId, setUserId] = useState<string | null>();
    const [userActiveFavoriteClubAdvertisements, setUserActiveFavoriteClubAdvertisements] = useState<FavoriteClubAdvertisement[]>([]);
    const [userInactiveFavoriteClubAdvertisements, setUserInactiveFavoriteClubAdvertisements] = useState<FavoriteClubAdvertisement[]>([]);
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
                    const _userActiveFavoriteClubAdvertisements = await UserService.getUserActiveClubAdvertisementFavorites(userId);
                    setUserActiveFavoriteClubAdvertisements(_userActiveFavoriteClubAdvertisements);

                    const _userInactiveFavoriteClubAdvertisements = await UserService.getUserInactiveClubAdvertisementFavorites(userId);
                    setUserInactiveFavoriteClubAdvertisements(_userInactiveFavoriteClubAdvertisements);
                }
            }
            catch (error) {
                console.error('Failed to fetch user\'s data:', error);
                toast.error('Failed to load user\'s data.');
            }
        };

        fetchUserData();
    }, [location]);

    const moveToClubAdvertisementPage = (clubAdvertisementId: number) => {
        navigate(`/club-advertisement/${clubAdvertisementId}`, { state: { clubAdvertisementId } });
    };

    const handleShowDeleteModal = (favoriteAdvertisementId: number) => {
        setDeleteFavoriteId(favoriteAdvertisementId);
        setShowDeleteModal(true);
    };

    const handleDeleteFromFavorites = async () => {
        if (!userId || !deleteFavoriteId)
            return;

        try {
            await FavoriteClubAdvertisementService.deleteFromFavorites(deleteFavoriteId);
            toast.success('Your followed advertisement has been deleted from favorites successfully.');
            setShowDeleteModal(false);
            setDeleteFavoriteId(null);
            // Refresh the user data
            const _userActiveFavoriteClubAdvertisements = await UserService.getUserActiveClubAdvertisementFavorites(userId);
            setUserActiveFavoriteClubAdvertisements(_userActiveFavoriteClubAdvertisements);

            const _userInactiveFavoriteClubAdvertisements = await UserService.getUserInactiveClubAdvertisementFavorites(userId);
            setUserInactiveFavoriteClubAdvertisements(_userInactiveFavoriteClubAdvertisements);
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

    const calculateSkippedDays = (endDate: string): number => {
        const currentDate = new Date();
        const end = new Date(endDate);
        const timeDiff = currentDate.getTime() - end.getTime();
        const days = timeDiff / (1000 * 3600 * 24);
        return Math.floor(days);
    };

    return (
        <div className="MyFavoriteClubAdvertisements">
            <ToastContainer />
            <h1>My Favorite Club Advertisements</h1>
            {/* Active favorite advertisements*/}
            <h3>Active advertisements</h3>
            <div className="table-responsive">
                <Table striped bordered hover variant="light">
                    <thead className="table-success">
                        <tr>
                            <th>End Date (days left)</th>
                            <th>Club Name</th>
                            <th>League (Region)</th>
                            <th>Position</th>
                            <th>Salary (zł.) / month</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {userActiveFavoriteClubAdvertisements.length > 0 ? (
                            userActiveFavoriteClubAdvertisements.map((favoriteAdvertisement, index) => (
                                <tr key={index}>
                                    <td>{formatDate(favoriteAdvertisement.clubAdvertisement.endDate)} ({calculateDaysLeft(favoriteAdvertisement.clubAdvertisement.endDate)} days)</td>
                                    <td>{favoriteAdvertisement.clubAdvertisement.clubName}</td>
                                    <td>{favoriteAdvertisement.clubAdvertisement.league} ({favoriteAdvertisement.clubAdvertisement.region})</td>
                                    <td>{favoriteAdvertisement.clubAdvertisement.playerPosition.positionName}</td>
                                    <td>{favoriteAdvertisement.clubAdvertisement.salaryRange.min} - {favoriteAdvertisement.clubAdvertisement.salaryRange.max}</td>
                                    <td>
                                        <Button variant="dark" className="button-spacing" onClick={() => moveToClubAdvertisementPage(favoriteAdvertisement.clubAdvertisement.id)}>
                                            <i className="bi bi-info-square"></i>
                                        </Button>
                                        <Button variant="danger" onClick={() => handleShowDeleteModal(favoriteAdvertisement.id)}>
                                            <i className="bi bi-heart-fill"></i>
                                        </Button>
                                    </td>
                                </tr>
                            ))
                        ) : (
                            <tr>
                                <td colSpan={9} className="text-center">No favorite club advertisement available</td>
                            </tr>
                        )}
                    </tbody>
                </Table>
            </div>

            {/* Inactive favorite advertisements*/}
            <h3>Archived advertisements</h3>
            <div className="table-responsive">
                <Table striped bordered hover variant="light">
                    <thead className="table-warning">
                        <tr>
                            <th>End Date (days passed)</th>
                            <th>Club Name</th>
                            <th>League (Region)</th>
                            <th>Position</th>
                            <th>Salary (zł.) / month</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {userInactiveFavoriteClubAdvertisements.length > 0 ? (
                            userInactiveFavoriteClubAdvertisements.map((favoriteAdvertisement, index) => (
                                <tr key={index}>
                                    <td>{formatDate(favoriteAdvertisement.clubAdvertisement.endDate)} ({calculateSkippedDays(favoriteAdvertisement.clubAdvertisement.endDate)} days)</td>
                                    <td>{favoriteAdvertisement.clubAdvertisement.clubName}</td>
                                    <td>{favoriteAdvertisement.clubAdvertisement.league} ({favoriteAdvertisement.clubAdvertisement.region})</td>
                                    <td>{favoriteAdvertisement.clubAdvertisement.playerPosition.positionName}</td>
                                    <td>{favoriteAdvertisement.clubAdvertisement.salaryRange.min} - {favoriteAdvertisement.clubAdvertisement.salaryRange.max}</td>
                                    <td>
                                        <Button variant="dark" className="button-spacing" onClick={() => moveToClubAdvertisementPage(favoriteAdvertisement.clubAdvertisement.id)}>
                                            <i className="bi bi-info-square"></i>
                                        </Button>
                                        <Button variant="danger" onClick={() => handleShowDeleteModal(favoriteAdvertisement.id)}>
                                            <i className="bi bi-heart-fill"></i>
                                        </Button>
                                    </td>
                                </tr>
                            ))
                        ) : (
                            <tr>
                                <td colSpan={9} className="text-center">No favorite club advertisement available</td>
                            </tr>
                        )}
                    </tbody>
                </Table>
            </div>

            {/* Delete Favorite Club Advertisement Modal */}
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

export default MyFavoriteClubAdvertisements;