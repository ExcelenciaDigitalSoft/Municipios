import axios from 'axios';
import { catalogosApi } from './catalogos';
import { gestionApi } from './gestion';
import { authApi } from './auth';

const API_URL = import.meta.env.VITE_API_URL || '';

// Crear instancia de axios configurada
const apiClient = axios.create({
  baseURL: API_URL,
  withCredentials: true, // Para enviar cookies en peticiones cross-origin
  headers: {
    'Content-Type': 'application/json',
    'Accept': 'application/json'
  }
});

// Interceptores para manejo global de errores
apiClient.interceptors.response.use(
  response => response,
  error => {
    const { status } = error.response || {};
    
    if (status === 401) {
      // Redireccionar a login si la sesión expiró
      window.location = '/login';
    }
    
    if (status === 500) {
      console.error('Error del servidor:', error);
      // Notificar al usuario
      // toast.error('Ocurrió un error en el servidor');
    }
    
    return Promise.reject(error);
  }
);

export { apiClient, catalogosApi, gestionApi, authApi };
