interface UserUpdateDTO {
    id: string;
    email: string;
    passwordHash: string;
    confirmPasswordHash: string;
    firstName: string;
    lastName: string;
    phoneNumber: string;
    location: string;
}

export default UserUpdateDTO;