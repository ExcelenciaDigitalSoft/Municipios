import { apiClient } from './index';

export const authApi = {
  login: (credentials) => apiClient.post('/api/auth/login', credentials),
  logout: () => apiClient.post('/api/auth/logout'),
  getPerfil: () => apiClient.get('/api/auth/perfil')
};