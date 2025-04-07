<template>
    <div class="p-4">
      <div class="mb-4">
        <h2 class="text-2xl font-bold">Cuotas del Plan de Pago</h2>
        <form @submit.prevent="crearCuota" class="grid grid-cols-2 gap-4 mt-4">
          <input v-model="nuevo.idPlan" placeholder="ID Plan" class="input" type="number" />
          <input v-model="nuevo.numeroCuota" placeholder="Número de Cuota" class="input" type="number" />
          <input v-model="nuevo.fechaVencimiento" type="date" class="input" />
          <input v-model="nuevo.montoCuota" type="number" step="0.01" placeholder="Monto Cuota" class="input" />
          <label class="col-span-2"><input type="checkbox" v-model="nuevo.pagada" /> Pagada</label>
          <button type="submit" class="btn col-span-2">{{ editar ? 'Actualizar' : 'Crear' }}</button>
        </form>
      </div>
  
      <div class="card mt-4">
        <ul>
          <li v-for="c in cuotas" :key="c.id" class="py-2 border-b flex justify-between items-center">
            <span>Plan: {{ c.idPlan }} - Cuota: {{ c.numeroCuota }} - ${{ c.montoCuota }} - {{ c.fechaVencimiento }}</span>
            <div class="space-x-2">
              <button class="btn" @click="seleccionarEditar(c)">Editar</button>
              <button class="btn bg-red-500" @click="eliminarCuota(c.id)">Eliminar</button>
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
      const cuotas = ref([]);
      const editar = ref(false);
      const nuevo = ref({
        id: null,
        idPlan: null,
        numeroCuota: null,
        fechaVencimiento: '',
        montoCuota: 0.00,
        pagada: false
      });
  
      const token = localStorage.getItem('token');
      const headers = { Authorization: `Bearer ${token}` };
  
      const cargarCuotas = async () => {
        const res = await axios.get('/api/admin/gestion/cuotasplan', { headers });
        cuotas.value = res.data;
      };
  
      const crearCuota = async () => {
        if (editar.value) {
          await axios.put(`/api/cuotasplan/${nuevo.value.id}`, nuevo.value, { headers });
        } else {
          await axios.post('/api/admin/gestion/cuotasplan', nuevo.value, { headers });
        }
        await cargarCuotas();
        limpiarFormulario();
      };
  
      const eliminarCuota = async (id) => {
        await axios.delete(`/api/admin/gestion/cuotasplan/${id}`, { headers });
        await cargarCuotas();
      };
  
      const seleccionarEditar = (c) => {
        Object.assign(nuevo.value, { ...c });
        editar.value = true;
      };
  
      const limpiarFormulario = () => {
        Object.assign(nuevo.value, {
          id: null,
          idPlan: null,
          numeroCuota: null,
          fechaVencimiento: '',
          montoCuota: 0.00,
          pagada: false
        });
        editar.value = false;
      };
  
      onMounted(() => cargarCuotas());
  
      return {
        cuotas,
        nuevo,
        editar,
        crearCuota,
        eliminarCuota,
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
  