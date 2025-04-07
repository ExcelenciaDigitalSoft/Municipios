<template>
    <div class="p-4">
      <div class="mb-4">
        <h2 class="text-2xl font-bold">Medios de Pago</h2>
        <form @submit.prevent="crearMedioPago" class="grid grid-cols-2 gap-4 mt-4">
          <input v-model="nuevo.nombre" placeholder="Nombre" class="input" />
          <input v-model="nuevo.descripcion" placeholder="Descripción" class="input" />
          <textarea v-model="nuevo.condiciones" placeholder="Condiciones" class="input col-span-2"></textarea>
          <select v-model="nuevo.tipoIntegracion" class="input col-span-2">
            <option value="API">API</option>
            <option value="Archivo">Archivo</option>
            <option value="Manual">Manual</option>
          </select>
          <textarea v-model="nuevo.detallesIntegracion" placeholder="Detalles Integración" class="input col-span-2"></textarea>
          <button type="submit" class="btn col-span-2">{{ editar ? 'Actualizar' : 'Crear' }}</button>
        </form>
      </div>
  
      <div class="card mt-4">
        <ul>
          <li v-for="m in mediosPago" :key="m.id" class="py-2 border-b flex justify-between items-center">
            <span>{{ m.nombre }} - {{ m.tipoIntegracion }}</span>
            <div class="space-x-2">
              <button class="btn" @click="seleccionarEditar(m)">Editar</button>
              <button class="btn bg-red-500" @click="eliminarMedioPago(m.id)">Eliminar</button>
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
      const mediosPago = ref([]);
      const editar = ref(false);
      const nuevo = ref({
        id: null,
        nombre: '',
        descripcion: '',
        condiciones: '',
        tipoIntegracion: 'API',
        detallesIntegracion: ''
      });
  
      const token = localStorage.getItem('token');
      const headers = { Authorization: `Bearer ${token}` };
  
      const cargarMediosPago = async () => {
        const res = await axios.get('/api/admin/gestion/mediospago', { headers });
        mediosPago.value = res.data;
      };
  
      const crearMedioPago = async () => {
        if (editar.value) {
          await axios.put(`/api/mediospago/${nuevo.value.id}`, nuevo.value, { headers });
        } else {
          await axios.post('/api/admin/gestion/mediospago', nuevo.value, { headers });
        }
        await cargarMediosPago();
        limpiarFormulario();
      };
  
      const eliminarMedioPago = async (id) => {
        await axios.delete(`/api/admin/gestion/mediospago/${id}`, { headers });
        await cargarMediosPago();
      };
  
      const seleccionarEditar = (m) => {
        Object.assign(nuevo.value, { ...m });
        editar.value = true;
      };
  
      const limpiarFormulario = () => {
        Object.assign(nuevo.value, {
          id: null,
          nombre: '',
          descripcion: '',
          condiciones: '',
          tipoIntegracion: 'API',
          detallesIntegracion: ''
        });
        editar.value = false;
      };
  
      onMounted(() => cargarMediosPago());
  
      return {
        mediosPago,
        nuevo,
        editar,
        crearMedioPago,
        eliminarMedioPago,
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
  