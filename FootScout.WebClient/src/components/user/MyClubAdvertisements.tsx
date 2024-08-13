import React, { useEffect, useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { Table, Button } from 'react-bootstrap';
import { ToastContainer, toast } from 'react-toastify';
import AccountService from '../../services/api/AccountService';
import UserService from '../../services/api/UserService';
import ClubAdvertisement from '../../models/interfaces/ClubAdvertisement';
import '../../App.css';
import '../../styles/user/MyClubAdvertisements.css';

const MyClubAdvertisements = () => {
    const navigate = useNavigate();
    const location = useLocation();
    const [userActiveClubAdvertisements, setUserActiveClubAdvertisements] = useState<ClubAdvertisement[]>([]);
    const [userInactiveClubAdvertisements, setUserInactiveClubAdvertisements] = useState<ClubAdvertisement[]>([]);

    useEffect(() => {
        if (location.state && location.state.toastMessage)
            toast.success(location.state.toastMessage);

        const fetchUserClubAdvertisements = async () => {
            try {
                const userId = await AccountService.getId();
                if (userId) {
                    const _userActiveClubAdvertisements = await UserService.getUserActiveClubAdvertisements(userId);
                    setUserActiveClubAdvertisements(_userActiveClubAdvertisements);

                    const _userInactiveClubAdvertisements = await UserService.getUserInactiveClubAdvertisements(userId);
                    setUserInactiveClubAdvertisements(_userInactiveClubAdvertisements);
                }
            }
            catch (error) {
                console.error('Failed to fetch user\'s club advertisements:', error);
                toast.error('Failed to load user\'s club advertisements.');
            }
        };

        fetchUserClubAdvertisements();
    }, [location]);

    const moveToClubAdvertisementPage = (clubAdvertisementId: number) => {
        navigate(`/club-advertisement/${clubAdvertisementId}`, { state: { clubAdvertisementId } });
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
        <div className="MyClubAdvertisements">
            <ToastContainer />
            <h1>My Club Advertisements</h1>
            <Button variant="success" className="form-button" onClick={() => navigate('/new-club-advertisement')}>
                <i className="bi bi-file-earmark-plus-fill"></i>
                New Advertisement
            </Button>
            {/* Active advertisements*/}
            <h3>Active advertisements</h3>
            <div className="table-responsive">
                <Table striped bordered hover variant="light">
                    <thead className="table-success">
                        <tr>
                            <th>Creation Date (days left)</th>
                            <th>Position</th>
                            <th>Club Name</th>
                            <th>League (Region)</th>
                            <th>Salary (zł.) / month</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {userActiveClubAdvertisements.length > 0 ? (
                            userActiveClubAdvertisements.map((advertisement, index) => (
                                <tr key={index}>
                                    <td>{formatDate(advertisement.creationDate)} ({calculateDaysLeft(advertisement.endDate)} days)</td>
                                    <td>{advertisement.playerPosition.positionName}</td>
                                    <td>{advertisement.clubName}</td>
                                    <td>{advertisement.league} ({advertisement.region})</td>
                                    <td>{advertisement.salaryRange.min} - {advertisement.salaryRange.max}</td>
                                    <td>
                                        <Button variant="dark" className="button-spacing" onClick={() => moveToClubAdvertisementPage(advertisement.id)}>
                                            <i className="bi bi-info-square"></i>
                                        </Button>
                                    </td>
                                </tr>
                            ))
                        ) : (
                            <tr>
                                <td colSpan={9} className="text-center">No club advertisement available</td>
                            </tr>
                        )}
                    </tbody>
                </Table>
            </div>

            {/* Inactive advertisements*/}
            <h3>Archived advertisements</h3>
            <div className="table-responsive">
                <Table striped bordered hover variant="light">
                    <thead className="table-warning">
                        <tr>
                        <th>Creation Date (days left)</th>
                            <th>Position</th>
                            <th>Club Name</th>
                            <th>League (Region)</th>
                            <th>Salary (zł.) / month</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {userInactiveClubAdvertisements.length > 0 ? (
                            userInactiveClubAdvertisements.map((advertisement, index) => (
                                <tr key={index}>
                                    <td>{formatDate(advertisement.creationDate)} ({calculateSkippedDays(advertisement.endDate)} days)</td>
                                    <td>{advertisement.playerPosition.positionName}</td>
                                    <td>{advertisement.clubName}</td>
                                    <td>{advertisement.league} ({advertisement.region})</td>
                                    <td>{advertisement.salaryRange.min} - {advertisement.salaryRange.max}</td>
                                    <td>
                                        <Button variant="dark" className="button-spacing" onClick={() => moveToClubAdvertisementPage(advertisement.id)}>
                                            <i className="bi bi-info-square"></i>
                                        </Button>
                                    </td>
                                </tr>
                            ))
                        ) : (
                            <tr>
                                <td colSpan={9} className="text-center">No club advertisement available</td>
                            </tr>
                        )}
                    </tbody>
                </Table>
            </div>
        </div>
    );
}

export default MyClubAdvertisements;