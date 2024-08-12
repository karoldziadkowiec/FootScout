import React, { useEffect, useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { Table, Button } from 'react-bootstrap';
import { ToastContainer, toast } from 'react-toastify';
import AccountService from '../../services/api/AccountService';
import UserService from '../../services/api/UserService';
import PlayerAdvertisement from '../../models/interfaces/PlayerAdvertisement';
import '../../App.css';
import '../../styles/user/MyPlayerAdvertisements.css';

const MyPlayerAdvertisements = () => {
    const navigate = useNavigate();
    const location = useLocation();
    const [userActivePlayerAdvertisements, setUserActivePlayerAdvertisements] = useState<PlayerAdvertisement[]>([]);
    const [userInactivePlayerAdvertisements, setUserInactivePlayerAdvertisements] = useState<PlayerAdvertisement[]>([]);

    useEffect(() => {
        if (location.state && location.state.toastMessage)
            toast.success(location.state.toastMessage);

        const fetchUserPlayerAdvertisements = async () => {
            try {
                const userId = await AccountService.getId();
                if (userId) {
                    const _userActivePlayerAdvertisements = await UserService.getUserActivePlayerAdvertisements(userId);
                    setUserActivePlayerAdvertisements(_userActivePlayerAdvertisements);

                    const _userInactivePlayerAdvertisements = await UserService.getUserInactivePlayerAdvertisements(userId);
                    setUserInactivePlayerAdvertisements(_userInactivePlayerAdvertisements);
                }
            }
            catch (error) {
                console.error('Failed to fetch user\'s player advertisements:', error);
                toast.error('Failed to load user\'s player advertisements.');
            }
        };

        fetchUserPlayerAdvertisements();
    }, [location]);

    const moveToPlayerAdvertisementPage = (playerAdvertisementId: number) => {
        navigate(`/player-advertisement/${playerAdvertisementId}`, { state: { playerAdvertisementId } });
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
        <div className="MyPlayerAdvertisements">
            <ToastContainer />
            <h1>My Player Advertisements</h1>
            <Button variant="success" className="form-button" onClick={() => navigate('/new-player-advertisement')}>
                <i className="bi bi-file-earmark-plus-fill"></i>
                New Advertisement
            </Button>
            {/* Active advertisements*/}
            <h3>Active advertisements</h3>
            <div className="table-responsive">
                <Table striped bordered hover variant="success">
                    <thead className="table-dark">
                        <tr>
                            <th>Creation Date (days left)</th>
                            <th>Position</th>
                            <th>Preferred League</th>
                            <th>Region</th>
                            <th>Salary (zł.) / month</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {userActivePlayerAdvertisements.length > 0 ? (
                            userActivePlayerAdvertisements.map((advertisement, index) => (
                                <tr key={index}>
                                    <td>{formatDate(advertisement.creationDate)} ({calculateDaysLeft(advertisement.endDate)} days)</td>
                                    <td>{advertisement.playerPosition.positionName}</td>
                                    <td>{advertisement.league}</td>
                                    <td>{advertisement.region}</td>
                                    <td>{advertisement.salaryRange.min} - {advertisement.salaryRange.max}</td>
                                    <td>
                                        <Button variant="dark" className="button-spacing" onClick={() => moveToPlayerAdvertisementPage(advertisement.id)}>
                                            <i className="bi bi-info-square"></i>
                                        </Button>
                                    </td>
                                </tr>
                            ))
                        ) : (
                            <tr>
                                <td colSpan={9} className="text-center">No player advertisement available</td>
                            </tr>
                        )}
                    </tbody>
                </Table>
            </div>

            {/* Inactive advertisements*/}
            <h3>Archived advertisements</h3>
            <div className="table-responsive">
                <Table striped bordered hover variant="warning">
                    <thead className="table-dark">
                        <tr>
                            <th>Ended Date (days ago)</th>
                            <th>Position</th>
                            <th>Preferred League</th>
                            <th>Region</th>
                            <th>Salary (zł.) / month</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {userInactivePlayerAdvertisements.length > 0 ? (
                            userInactivePlayerAdvertisements.map((advertisement, index) => (
                                <tr key={index}>
                                    <td>{formatDate(advertisement.endDate)} ({calculateSkippedDays(advertisement.endDate)} days)</td>
                                    <td>{advertisement.playerPosition.positionName}</td>
                                    <td>{advertisement.league}</td>
                                    <td>{advertisement.region}</td>
                                    <td>{advertisement.salaryRange.min} - {advertisement.salaryRange.max}</td>
                                    <td>
                                        <Button variant="dark" className="button-spacing" onClick={() => moveToPlayerAdvertisementPage(advertisement.id)}>
                                            <i className="bi bi-info-square"></i>
                                        </Button>
                                    </td>
                                </tr>
                            ))
                        ) : (
                            <tr>
                                <td colSpan={9} className="text-center">No player advertisement available</td>
                            </tr>
                        )}
                    </tbody>
                </Table>
            </div>
        </div>
    );
}

export default MyPlayerAdvertisements;