import { apiClient } from './index';

export const catalogosApi = {
  // Métodos para Estados
  getEstados: () => apiClient.get('/api/admin/catalogos/estados'),
  createEstado: (data) => apiClient.post('/api/admin/catalogos/estados', data),
  updateEstado: (id, data) => apiClient.put(`/api/admin/catalogos/estados/${id}`, data),
  deleteEstado: (id) => apiClient.delete(`/api/admin/catalogos/estados/${id}`),
  
  // Métodos para Estados de Tributo
  getEstadosTributo: () => apiClient.get('/api/admin/catalogos/estados-tributo'),
  createEstadoTributo: (data) => apiClient.post('/api/admin/catalogos/estados-tributo', data),
  updateEstadoTributo: (id, data) => apiClient.put(`/api/admin/catalogos/estados-tributo/${id}`, data),
  deleteEstadoTributo: (id) => apiClient.delete(`/api/admin/catalogos/estados-tributo/${id}`),
  
  // Y así sucesivamente para cada entidad
};
