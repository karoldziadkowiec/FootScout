import React, { useEffect, useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { Table, Button, Modal, Form } from 'react-bootstrap';
import { ToastContainer, toast } from 'react-toastify';
import AccountService from '../../services/api/AccountService';
import UserService from '../../services/api/UserService';
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
            const updatedFormData = {
                ...receivedClubOfferToAccept
            };

            await ClubOfferService.acceptClubOffer(receivedClubOfferToAccept.id, updatedFormData);
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
            const updatedFormData = {
                ...receivedClubOfferToReject
            };

            await ClubOfferService.rejectClubOffer(receivedClubOfferToReject.id, updatedFormData);
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
            {/* Received offers from clubs*/}
            <h3>Received offers from clubs</h3>
            <div className="table-responsive">
                <Table striped bordered hover variant="light">
                    <thead className="table-dark">
                        <tr>
                            <th>Received Date</th>
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
                                    <td className="offer-row">{formatDate(clubOffer.creationDate)}</td>
                                    <td className="offer-row">{clubOffer.offerStatus.statusName}</td>
                                    <td className="offer-row">{clubOffer.clubName}</td>
                                    <td className="offer-row">{clubOffer.league} ({clubOffer.region})</td>
                                    <td className="offer-row">{clubOffer.playerPosition.positionName}</td>
                                    <td className="offer-row">
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
                                <td colSpan={9} className="text-center">No received club offer available</td>
                            </tr>
                        )}
                    </tbody>
                </Table>
            </div>

            {/* My sent offers to clubs*/}
            <h3>My sent requests to clubs</h3>
            <div className="table-responsive">

            </div>

            {/* Details of Received Club Offer */}
            <Modal size="lg" show={showReceivedClubOfferDetailsModal} onHide={() => setShowReceivedClubOfferDetailsModal(false)}>
                <Modal.Header closeButton>
                    <Modal.Title>Offer Details</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    {selectedReceivedClubOffer && (
                        <div className="modal-content-centered">
                            <p><Form.Label className="clubOffer-name-label">{(selectedReceivedClubOffer.clubName).toUpperCase()}</Form.Label></p>
                            <p><Form.Label className="clubOffer-position-label">{selectedReceivedClubOffer.playerPosition.positionName}</Form.Label></p>
                            <Form.Label className="clubOffer-section">OFFER INFO</Form.Label>
                            <p><strong>Received Date</strong> {formatDate(selectedReceivedClubOffer.creationDate)}</p>
                            <p><strong>End Date (days left/passed)</strong> {formatDate(selectedReceivedClubOffer.playerAdvertisement.endDate)} ({calculateDaysLeft(selectedReceivedClubOffer.playerAdvertisement.endDate)})</p>
                            <p><strong>Offer status:</strong> {selectedReceivedClubOffer.offerStatus.statusName}</p>
                            <Form.Label className="clubOffer-section">DETAILS</Form.Label>
                            <p><strong>Club Name:</strong> {selectedReceivedClubOffer.clubName}</p>
                            <p><strong>League:</strong> {selectedReceivedClubOffer.league}</p>
                            <p><strong>Region:</strong> {selectedReceivedClubOffer.region}</p>
                            <p><strong>Position:</strong> {selectedReceivedClubOffer.playerPosition.positionName}</p>
                            <p><strong>Salary (zł.) / month:</strong> {selectedReceivedClubOffer.salary}</p>
                            <p><strong>Additional Information:</strong> {selectedReceivedClubOffer.additionalInformation}</p>
                            <Form.Label className="clubOffer-section">RECEIVED FROM</Form.Label>
                            <p><strong>Name:</strong> {selectedReceivedClubOffer.clubMember.firstName} {selectedReceivedClubOffer.clubMember.lastName}</p>
                            <p><strong>E-mail:</strong> {selectedReceivedClubOffer.clubMember.email}</p>
                            <p><strong>Phone number:</strong> {selectedReceivedClubOffer.clubMember.phoneNumber}</p>
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