interface ClubOfferCreateDTO {
    playerAdvertisementId: number;
    playerPositionId: number;
    clubName: string;
    league: string;
    region: string;
    salary: number;
    additionalInformation: string;
    userClubId: string;
}

export default ClubOfferCreateDTO;