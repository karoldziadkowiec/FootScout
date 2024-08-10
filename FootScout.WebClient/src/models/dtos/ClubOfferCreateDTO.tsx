interface ClubOfferCreateDTO {
    playerAdvertisementId: number;
    playerPositionId: number;
    clubName: string;
    league: string;
    region: string;
    salary: number;
    additionalInformation: string;
    endDate: string;
    userClubId: string;
}

export default ClubOfferCreateDTO;