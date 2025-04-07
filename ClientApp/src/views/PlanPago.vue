<template>
    <div class="p-4">
      <div class="mb-4">
        <h2 class="text-2xl font-bold">Planes de Pago</h2>
        <form @submit.prevent="crearPlanPago" class="grid grid-cols-2 gap-4 mt-4">
          <input v-model="nuevo.idContribuyente" placeholder="ID Contribuyente" class="input" type="number" />
          <input v-model="nuevo.descripcion" placeholder="Descripción del Plan" class="input" />
          <input v-model="nuevo.fechaInicio" type="date" class="input" />
          <input v-model="nuevo.fechaFin" type="date" class="input" />
          <input v-model="nuevo.montoTotal" type="number" step="0.01" placeholder="Monto Total" class="input" />
          <button type="submit" class="btn col-span-2">{{ editar ? 'Actualizar' : 'Crear' }}</button>
        </form>
      </div>
  
      <div class="card mt-4">
        <ul>
          <li v-for="p in planes" :key="p.id" class="py-2 border-b flex justify-between items-center">
            <span>{{ p.descripcion }} - Total: ${{ p.montoTotal }}</span>
            <div class="space-x-2">
              <button class="btn" @click="seleccionarEditar(p)">Editar</button>
              <button class="btn bg-red-500" @click="eliminarPlanPago(p.id)">Eliminar</button>
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
      const planes = ref([]);
      const editar = ref(false);
      const nuevo = ref({
        id: null,
        idContribuyente: null,
        descripcion: '',
        fechaInicio: '',
        fechaFin: '',
        montoTotal: 0.00
      });
  
      const token = localStorage.getItem('token');
      const headers = { Authorization: `Bearer ${token}` };
  
      const cargarPlanes = async () => {
        const res = await axios.get('/api/admin/gestion/planespago', { headers });
        planes.value = res.data;
      };
  
      const crearPlanPago = async () => {
        if (editar.value) {
          await axios.put(`/api/planespago/${nuevo.value.id}`, nuevo.value, { headers });
        } else {
          await axios.post('/api/admin/gestion/planespago', nuevo.value, { headers });
        }
        await cargarPlanes();
        limpiarFormulario();
      };
  
      const eliminarPlanPago = async (id) => {
        await axios.delete(`/api/admin/gestion/planespago/${id}`, { headers });
        await cargarPlanes();
      };
  
      const seleccionarEditar = (p) => {
        Object.assign(nuevo.value, { ...p });
        editar.value = true;
      };
  
      const limpiarFormulario = () => {
        Object.assign(nuevo.value, {
          id: null,
          idContribuyente: null,
          descripcion: '',
          fechaInicio: '',
          fechaFin: '',
          montoTotal: 0.00
        });
        editar.value = false;
      };
  
      onMounted(() => cargarPlanes());
  
      return {
        planes,
        nuevo,
        editar,
        crearPlanPago,
        eliminarPlanPago,
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
  