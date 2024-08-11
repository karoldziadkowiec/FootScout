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
import '../../styles/user/MyOffersAsPlayer.css';


const MyOffersAsPlayer = () => {
    const navigate = useNavigate();
    const location = useLocation();
    const [userId, setUserId] = useState<string | null>(null);
    const [receivedClubOffers, setReceivedClubOffers] = useState<ClubOffer[]>([]);
    const [showReceivedClubOfferDetailsModal, setShowReceivedClubOfferDetailsModal] = useState<boolean>(false);
    const [showAcceptReceivedClubOfferModal, setShowAcceptReceivedClubOfferModal] = useState<boolean>(false);
    const [showRejectReceivedClubOfferModal, setShowRejectReceivedClubOfferModal] = useState<boolean>(false);
    const [selectedReceivedClubOffer, setSelectedReceivedClubOffer] = useState<ClubOffer | null>(null);
    const [receivedClubOfferToAccept, setReceivedClubOfferToAccept] = useState<ClubOffer | null>(null);
    const [receivedClubOfferToReject, setReceivedClubOfferToReject] = useState<ClubOffer | null>(null);

    useEffect(() => {
        if (location.state && location.state.toastMessage)
            toast.success(location.state.toastMessage);

        const fetchUserData = async () => {
            try {
                const userId = await AccountService.getId();
                if (userId) {
                    setUserId(userId);
                    const _receivedClubOffers = await UserService.getReceivedClubOffers(userId);
                    setReceivedClubOffers(_receivedClubOffers);
                }
            }
            catch (error) {
                console.error('Failed to fetch user\'s data:', error);
                toast.error('Failed to load user\'s data.');
            }
        };

        fetchUserData();
    }, [location]);

    const handleShowReceivedClubOfferDetails = (clubOffer: ClubOffer) => {
        setSelectedReceivedClubOffer(clubOffer);
        setShowReceivedClubOfferDetailsModal(true);
    };

    const handleShowAcceptReceivedClubOfferModal = (clubOffer: ClubOffer) => {
        setReceivedClubOfferToAccept(clubOffer);
        setShowAcceptReceivedClubOfferModal(true);
    };

    const handleAcceptReceivedClubOffer = async () => {
        if (!receivedClubOfferToAccept || !userId)
            return;

        try {
            const statusId = await OfferStatusService.getOfferStatusId(OfferStatusName.Accepted);
            const _offerStatus = await OfferStatusService.getOfferStatus(statusId);

            const updatedFormData = {
                ...receivedClubOfferToAccept,
                offerStatusId: statusId,
                offerStatus: _offerStatus
            };

            await ClubOfferService.updateClubOffer(receivedClubOfferToAccept.id, updatedFormData);
            setShowAcceptReceivedClubOfferModal(false);
            toast.success('Received club offer has been accepted successfully.');
            // Refresh data
            const _receivedClubOffers = await UserService.getReceivedClubOffers(userId);
            setReceivedClubOffers(_receivedClubOffers);
        }
        catch (error) {
            console.error('Failed to accept received club offer:', error);
            toast.error('Failed to accept received club offer.');
        }
    };

    const handleShowRejectReceivedClubOfferModal = (clubOffer: ClubOffer) => {
        setReceivedClubOfferToReject(clubOffer);
        setShowRejectReceivedClubOfferModal(true);
    };

    const handleRejectReceivedClubOffer = async () => {
        if (!receivedClubOfferToReject || !userId)
            return;

        try {
            const statusId = await OfferStatusService.getOfferStatusId(OfferStatusName.Rejected);
            const _offerStatus = await OfferStatusService.getOfferStatus(statusId);

            const updatedFormData = {
                ...receivedClubOfferToReject,
                offerStatusId: statusId,
                offerStatus: _offerStatus
            };

            await ClubOfferService.updateClubOffer(receivedClubOfferToReject.id, updatedFormData);
            setShowRejectReceivedClubOfferModal(false);
            toast.success('Received club offer has been rejected successfully.');
            // Refresh data
            const _receivedClubOffers = await UserService.getReceivedClubOffers(userId);
            setReceivedClubOffers(_receivedClubOffers);
        }
        catch (error) {
            console.error('Failed to reject received club offer:', error);
            toast.error('Failed to reject received club offer.');
        }
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
        <div className="MyOffersAsPlayer">
            <ToastContainer />
            <h1>My Offers as a Player</h1>
            <div className="table-responsive">
                {/* Received offers from clubs*/}
                <h3>Received offers from clubs</h3>
                <Table striped bordered hover>
                    <thead>
                        <tr>
                            <th>Received Date (days left)</th>
                            <th>Offer Status</th>
                            <th>Club Name</th>
                            <th>League (Region)</th>
                            <th>Position</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {receivedClubOffers.length > 0 ? (
                            receivedClubOffers.map((clubOffer, index) => (
                                <tr key={index}>
                                    <td>{formatDate(clubOffer.creationDate)} ({calculateDaysLeft(clubOffer.playerAdvertisement.endDate)} days)</td>
                                    <td>{clubOffer.offerStatus.statusName}</td>
                                    <td>{clubOffer.clubName}</td>
                                    <td>{clubOffer.league} ({clubOffer.region})</td>
                                    <td>{clubOffer.playerPosition.positionName}</td>
                                    <td>
                                        <Button variant="primary" className="button-spacing" onClick={() => handleShowReceivedClubOfferDetails(clubOffer)}>
                                            <i className="bi bi-info-circle"></i> Offer
                                        </Button>
                                        {clubOffer.offerStatus.statusName === OfferStatusName.Offered && (new Date(clubOffer.playerAdvertisement.endDate) > new Date()) && (
                                            <>
                                                <Button variant="success" className="button-spacing" onClick={() => handleShowAcceptReceivedClubOfferModal(clubOffer)}>
                                                    <i className="bi bi-check-lg"></i>
                                                </Button>
                                                <Button variant="danger" className="button-spacing" onClick={() => handleShowRejectReceivedClubOfferModal(clubOffer)}>
                                                    <i className="bi bi-x"></i>
                                                </Button>
                                            </>
                                        )}
                                        <span className="button-spacing">|</span>
                                        <Button variant="dark" className="button-spacing" onClick={() => moveToPlayerAdvertisementPage(clubOffer.playerAdvertisement.id)}>
                                            <i className="bi bi-info-square"></i> Ad
                                        </Button>
                                    </td>
                                </tr>
                            ))
                        ) : (
                            <tr>
                                <td colSpan={9} className="text-center">No club offer available</td>
                            </tr>
                        )}
                    </tbody>
                </Table>

                {/* My sent offers to clubs*/}
                <h3>My sent offers to clubs</h3>

            </div>

            {/* Details of Received Club Offer */}
            <Modal show={showReceivedClubOfferDetailsModal} onHide={() => setShowReceivedClubOfferDetailsModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Offer Details</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    {selectedReceivedClubOffer && (
                        <div className="modal-content-centered">
                            <p><strong>Received Date (days left)</strong> {formatDate(selectedReceivedClubOffer.creationDate)} ({calculateDaysLeft(selectedReceivedClubOffer.playerAdvertisement.endDate)} days)</p>
                            <p><strong>Offer status:</strong> {selectedReceivedClubOffer.offerStatus.statusName}</p>
                            <p><strong>Club Name:</strong> {selectedReceivedClubOffer.clubName}</p>
                            <p><strong>League:</strong> {selectedReceivedClubOffer.league}</p>
                            <p><strong>Region:</strong> {selectedReceivedClubOffer.region}</p>
                            <p><strong>Position:</strong> {selectedReceivedClubOffer.playerPosition.positionName}</p>
                            <p><strong>Salary (zł.) / month:</strong> {selectedReceivedClubOffer.salary}</p>
                            <p><strong>Additional Information:</strong> {selectedReceivedClubOffer.additionalInformation}</p>
                            <p><strong>RECEIVED FROM</strong></p>
                            <p><strong>Name:</strong> {selectedReceivedClubOffer.userClub.firstName} {selectedReceivedClubOffer.userClub.lastName}</p>
                            <p><strong>E-mail:</strong> {selectedReceivedClubOffer.userClub.email}</p>
                            <p><strong>Phone number:</strong> {selectedReceivedClubOffer.userClub.phoneNumber}</p>
                        </div>
                    )}
                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setShowReceivedClubOfferDetailsModal(false)}>Close</Button>
                </Modal.Footer>
            </Modal>

            {/* Accept Received Club Offer */}
            <Modal show={showAcceptReceivedClubOfferModal} onHide={() => setShowAcceptReceivedClubOfferModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Confirm action</Modal.Title>
                </Modal.Header>
                <Modal.Body>Are you sure you want to accept this club offer?</Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setShowAcceptReceivedClubOfferModal(false)}>Cancel</Button>
                    <Button variant="success" onClick={handleAcceptReceivedClubOffer}>Accept</Button>
                </Modal.Footer>
            </Modal>

            {/* Reject Received Club Offer */}
            <Modal show={showRejectReceivedClubOfferModal} onHide={() => setShowRejectReceivedClubOfferModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Confirm action</Modal.Title>
                </Modal.Header>
                <Modal.Body>Are you sure you want to reject this club offer?</Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={() => setShowRejectReceivedClubOfferModal(false)}>Cancel</Button>
                    <Button variant="danger" onClick={handleRejectReceivedClubOffer}>Reject</Button>
                </Modal.Footer>
            </Modal>
        </div>
    );
}

export default MyOffersAsPlayer;