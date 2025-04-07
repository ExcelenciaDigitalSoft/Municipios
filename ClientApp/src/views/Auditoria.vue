<template>
    <div class="p-4">
      <div class="mb-4">
        <h2 class="text-2xl font-bold">Auditoría del Sistema</h2>
        <div class="card mt-4">
          <ul>
            <li v-for="a in auditoria" :key="a.id" class="py-2 border-b">
              <div>
                <strong>Usuario:</strong> {{ a.idUsuario }}<br />
                <strong>Tabla:</strong> {{ a.tablaAfectada }} - <strong>Acción:</strong> {{ a.accion }}<br />
                <strong>Fecha:</strong> {{ a.fechaHora }} - <strong>IP:</strong> {{ a.ipUsuario }}<br />
                <strong>Detalle:</strong> {{ a.descripcion }}
              </div>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  import { ref, onMounted } from 'vue';
  import axios from 'axios';
  
  export default {
    setup() {
      const auditoria = ref([]);
      const token = localStorage.getItem('token');
      const headers = { Authorization: `Bearer ${token}` };
  
      const cargarAuditoria = async () => {
        const res = await axios.get('/api/admin/sistema/auditoria', { headers });
        auditoria.value = res.data;
      };
  
      onMounted(() => cargarAuditoria());
  
      return { auditoria };
    }
  };
  </script>
  
  <style scoped>
  .card {
    background: white;
    padding: 1rem;
    border-radius: 1rem;
    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
  }
  </style>
  