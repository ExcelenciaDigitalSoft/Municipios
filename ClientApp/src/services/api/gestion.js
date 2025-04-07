import { apiClient } from './index';

export const gestionApi = {
  // Contribuyentes
  getContribuyentes: () => apiClient.get('/api/admin/gestion/contribuyentes'),
  createContribuyente: (data) => apiClient.post('/api/admin/gestion/contribuyentes', data),
  updateContribuyente: (id, data) => apiClient.put(`/api/contribuyentes/${id}`, data),
  deleteContribuyente: (id) => apiClient.delete(`/api/admin/gestion/contribuyentes/${id}`),
  
  // Inmuebles
  getInmuebles: () => apiClient.get('/api/admin/gestion/inmuebles'),
  createInmueble: (data) => apiClient.post('/api/admin/gestion/inmuebles', data),
  updateInmueble: (id, data) => apiClient.put(`/api/inmuebles/${id}`, data),
  deleteInmueble: (id) => apiClient.delete(`/api/admin/gestion/inmuebles/${id}`),
  
  // Y así sucesivamente para cada entidad de gestión
};
