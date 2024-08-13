import React, { useEffect, useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { Table, Button, Modal } from 'react-bootstrap';
import { ToastContainer, toast } from 'react-toastify';
import AccountService from '../../services/api/AccountService';
import UserService from '../../services/api/UserService';
import OfferStatusService from '../../services/api/OfferStatusService';
import ClubOfferService from '../../services/api/ClubOfferService';
import OfferStatusName from '../../models/enums/OfferStatusName';
import ClubOffer from '../../models/interfaces/ClubOffer';
import '../../App.css';
import '../../styles/user/MyOffersAsClub.css';


const MyOffersAsClub = () => {
    const navigate = useNavigate();
    const location = useLocation();
    const [userId, setUserId] = useState<string | null>(null);
    const [sentClubOffers, setSentClubOffers] = useState<ClubOffer[]>([]);
    const [showSentClubOfferDetailsModal, setShowSentClubOfferDetailsModal] = useState<boolean>(false);
    const [selectedSentClubOffer, setSelectedSentClubOffer] = useState<ClubOffer | null>(null);

    useEffect(() => {
        if (location.state && location.state.toastMessage)
            toast.success(location.state.toastMessage);

        const fetchUserData = async () => {
            try {
                const userId = await AccountService.getId();
                if (userId) {
                    setUserId(userId);
                    const _sentClubOffers = await UserService.getSentClubOffers(userId);
                    setSentClubOffers(_sentClubOffers);
                }
            }
            catch (error) {
                console.error('Failed to fetch user\'s data:', error);
                toast.error('Failed to load user\'s data.');
            }
        };

        fetchUserData();
    }, [location]);

    const handleShowSentClubOfferDetails = (clubOffer: ClubOffer) => {
        setSelectedSentClubOffer(clubOffer);
        setShowSentClubOfferDetailsModal(true);
    };

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

    const calculateDaysLeft = (endDate: string): string => {
        const currentDate = new Date();
        const end = new Date(endDate);

        if (currentDate <= end) {
            const timeDiff = end.getTime() - currentDate.getTime();
            const daysLeft = timeDiff / (1000 * 3600 * 24);
            return `${Math.ceil(daysLeft)} days left`;
        }
        else {
            const timeDiff = currentDate.getTime() - end.getTime();
            const days = timeDiff / (1000 * 3600 * 24);
            return `${Math.floor(days)} days passed`;
        }
    };

    return (
        <div className="MyOffersAsClub">
            <ToastContainer />
            <h1>My Offers as a Club member</h1>
            {/* Received offers from players*/}
            <h3>Received offers from players</h3>
            <div className="table-responsive">

            </div>

            {/* My sent offers to players*/}
            <h3>My sent offers to players</h3>
            <div className="table-responsive">
                <Table striped bordered hover variant="light">
                    <thead className="table-success">
                        <tr>
                            <th>Sent Date (days left/passed)</th>
                            <th>Offer Status</th>
                            <th>Player</th>
                            <th>Position</th>
                            <th>Club Name</th>
                            <th>League (Region)</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {sentClubOffers.length > 0 ? (
                            sentClubOffers.map((clubOffer, index) => (
                                <tr key={index}>
                                    <td className="offer-row">{formatDate(clubOffer.creationDate)} ({calculateDaysLeft(clubOffer.playerAdvertisement.endDate)})</td>
                                    <td className="offer-row">{clubOffer.offerStatus.statusName}</td>
                                    <td className="offer-row">{clubOffer.playerAdvertisement.user.firstName} {clubOffer.playerAdvertisement.user.lastName}</td>
                                    <td className="offer-row">{clubOffer.playerPosition.positionName}</td>
                                    <td className="offer-row">{clubOffer.clubName}</td>
                                    <td className="offer-row">{clubOffer.league} ({clubOffer.region})</td>
                                    <td className="offer-row">
                                        <Button variant="primary" className="button-spacing" onClick={() => handleShowSentClubOfferDetails(clubOffer)}>
                                            <i className="bi bi-info-circle"></i> Offer
                                        </Button>
                                        <span className="button-spacing">|</span>
                                        <Button variant="dark" className="button-spacing" onClick={() => moveToPlayerAdvertisementPage(clubOffer.playerAdvertisement.id)}>
                                            <i className="bi bi-info-square"></i> Ad
                                        </Button>
                                    </td>
                                </tr>
                            ))
                        ) : (
                            <tr>
                                <td colSpan={9} className="text-center">No sent club offer available</td>
                            </tr>
                        )}
                    </tbody>
                </Table>
            </div>

            {/* Details of Sent Club Offer */}
            <Modal show={showSentClubOfferDetailsModal} onHide={() => setShowSentClubOfferDetailsModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Offer Details</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    {selectedSentClubOffer && (
                        <div className="modal-content-centered">
                            <p><strong>Sent Date (days left/passed)</strong> {formatDate(selectedSentClubOffer.creationDate)} ({calculateDaysLeft(selectedSentClubOffer.playerAdvertisement.endDate)})</p>
                            <p><strong>Offer status:</strong> {selectedSentClubOffer.offerStatus.statusName}</p>
                            <p><strong>SENT TO</strong></p>
                            <p><strong>Name:</strong> {selectedSentClubOffer.playerAdvertisement.user.firstName} {selectedSentClubOffer.playerAdvertisement.user.lastName}</p>
                            <p><strong>E-mail:</strong> {selectedSentClubOffer.playerAdvertisement.user.email}</p>
                            <p><strong>Phone number:</strong> {selectedSentClubOffer.playerAdvertisement.user.phoneNumber}</p>
                            <p><strong>DETAILS</strong></p>
                            <p><strong>Club Name:</strong> {selectedSentClubOffer.clubName}</p>
                            <p><strong>League:</strong> {selectedSentClubOffer.league}</p>
                            <p><strong>Region:</strong> {selectedSentClubOffer.region}</p>
                            <p><strong>Position:</strong> {selectedSentClubOffer.playerPosition.positionName}</p>
                            <p><strong>Salary (zł.) / month:</strong> {selectedSentClubOffer.salary}</p>
                            <p><strong>Additional Information:</strong> {selectedSentClubOffer.additionalInformation}</p>
                        </div>
                    )}
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setShowSentClubOfferDetailsModal(false)}>Close</Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
}

export default MyOffersAsClub;