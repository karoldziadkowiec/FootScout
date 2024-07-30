import axios from 'axios';
import Cookies from 'js-cookie';
import ApiURL from '../../config/ApiConfig';
import LoginDTO from '../../models/dtos/LoginDTO';
import RegisterDTO from '../../models/dtos/RegisterDTO';
import jwtDecode from 'jwt-decode'
import Role from '../../models/enums/Role';

const AccountService = {
  async registerUser(registerDTO: RegisterDTO) {
    try {
      const response = await axios.post(`${ApiURL}/account/register`, registerDTO);
      return response.data;
    } catch (error) {
      console.error('Registration failed:', error);
      throw new Error('Registration failed');
    }
  },

  async login(loginDTO: LoginDTO) {
    try {
      const response = await axios.post(`${ApiURL}/account/login`, loginDTO);
      const token = response.data;

      if (token) {
        Cookies.set('AuthToken', token, { path: '/' });
        return token;
      } else {
        console.error('Token not found in response');
      }
    } catch (error) {
      console.error('Login failed:', error);
      throw new Error('Login failed');
    }
  },

  async getToken() {
    return Cookies.get('AuthToken') || null;
  },

  async getRole() {
    const token = await AccountService.getToken();
    if (token) {
      try {
        const decodedToken = jwtDecode<any>(token);
        return decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
      } catch (error) {
        console.error('Failed to decode token:', error);
      }
    }
    return null;
  },

  async getId() {
    const token = await AccountService.getToken();
    if (token) {
      try {
        const decodedToken = jwtDecode<any>(token);
        return decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
      } catch (error) {
        console.error('Failed to decode token:', error);
      }
    }
    return null;
  },

  async getEmail() {
    const token = await AccountService.getToken();
    if (token) {
      try {
        const decodedToken = jwtDecode<any>(token);
        return decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'];
      } catch (error) {
        console.error('Failed to decode token:', error);
      }
    }
    return null;
  },

  async isAuthenticated() {
    const token = await AccountService.getToken();
    return !!token;
  },

  async isAdmin(): Promise<boolean> {
    const role = await AccountService.getRole();
    if(role === Role.Admin)
      return true;
    else
      return false;
  },

  async logout() {
    Cookies.remove('AuthToken', { path: '/' });
  }
};

export default AccountService;