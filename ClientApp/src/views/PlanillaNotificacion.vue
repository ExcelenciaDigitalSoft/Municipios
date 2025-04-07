<template>
    <div class="p-4">
      <div class="mb-4">
        <h2 class="text-2xl font-bold">Plantillas de Notificación</h2>
        <form @submit.prevent="crearPlantilla" class="grid grid-cols-2 gap-4 mt-4">
          <input v-model="nuevo.titulo" placeholder="Título" class="input" />
          <textarea v-model="nuevo.cuerpo" placeholder="Cuerpo del mensaje" class="input col-span-2"></textarea>
          <select v-model="nuevo.canalDestino" class="input col-span-2">
            <option value="WhatsApp">WhatsApp</option>
            <option value="Email">Email</option>
            <option value="SMS">SMS</option>
          </select>
          <label class="col-span-2">
            <input type="checkbox" v-model="nuevo.activo" /> Activo
          </label>
          <button type="submit" class="btn col-span-2">{{ editar ? 'Actualizar' : 'Crear' }}</button>
        </form>
      </div>
  
      <div class="card mt-4">
        <ul>
          <li v-for="p in plantillas" :key="p.id" class="py-2 border-b flex justify-between items-center">
            <span>{{ p.titulo }} - {{ p.canalDestino }}</span>
            <div class="space-x-2">
              <button class="btn" @click="seleccionarEditar(p)">Editar</button>
              <button class="btn bg-red-500" @click="eliminarPlantilla(p.id)">Eliminar</button>
            </div>
          </li>
        </ul>
      </div>
    </div>
  </template>
  
  <script>
  import { ref, onMounted } from 'vue';
  import axios from 'axios';
  
  export default {
    setup() {
      const plantillas = ref([]);
      const editar = ref(false);
      const nuevo = ref({
        id: null,
        titulo: '',
        cuerpo: '',
        canalDestino: 'WhatsApp',
        activo: true
      });
  
      const token = localStorage.getItem('token');
      const headers = { Authorization: `Bearer ${token}` };
  
      const cargarPlantillas = async () => {
        const res = await axios.get('/api/admin/notificaciones/plantillas', { headers });
        plantillas.value = res.data;
      };
  
      const crearPlantilla = async () => {
        if (editar.value) {
          await axios.put(`/api/plantillasnotificacion/${nuevo.value.id}`, nuevo.value, { headers });
        } else {
          await axios.post('/api/admin/notificaciones/plantillas', nuevo.value, { headers });
        }
        await cargarPlantillas();
        limpiarFormulario();
      };
  
      const eliminarPlantilla = async (id) => {
        await axios.delete(`/api/admin/notificaciones/plantillas/${id}`, { headers });
        await cargarPlantillas();
      };
  
      const seleccionarEditar = (p) => {
        Object.assign(nuevo.value, { ...p });
        editar.value = true;
      };
  
      const limpiarFormulario = () => {
        Object.assign(nuevo.value, {
          id: null,
          titulo: '',
          cuerpo: '',
          canalDestino: 'WhatsApp',
          activo: true
        });
        editar.value = false;
      };
  
      onMounted(() => cargarPlantillas());
  
      return {
        plantillas,
        nuevo,
        editar,
        crearPlantilla,
        eliminarPlantilla,
        seleccionarEditar
      };
    }
  };
  </script>
  
  <style scoped>
  .input {
    padding: 0.5rem;
    border: 1px solid #ccc;
    border-radius: 0.5rem;
  }
  .btn {
    background: #2d6cdf;
    color: white;
    padding: 0.5rem;
    border-radius: 0.5rem;
    font-weight: bold;
  }
  .card {
    background: white;
    padding: 1rem;
    border-radius: 1rem;
    box-shadow: 0 2px 4px rgba(0,0,0,0.1);
  }
  </style>
  