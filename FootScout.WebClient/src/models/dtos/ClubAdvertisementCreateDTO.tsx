import SalaryRangeCreateDTO from "./SalaryRangeCreateDTO";

interface ClubAdvertisementCreateDTO {
    playerPositionId: number;
    clubName: string;
    league: string;
    region: string;
    salaryRangeDTO: SalaryRangeCreateDTO;
    userId: string;
}

export default ClubAdvertisementCreateDTO;