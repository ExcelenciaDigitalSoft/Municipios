<template>
    <div class="p-4">
      <div class="mb-4">
        <h2 class="text-2xl font-bold">Intereses por Mora</h2>
        <form @submit.prevent="crearInteres" class="grid grid-cols-2 gap-4 mt-4">
          <input v-model="nuevo.idTributo" placeholder="ID Tributo" class="input" type="number" />
          <input v-model="nuevo.diasMora" placeholder="Días de Mora" class="input" type="number" />
          <input v-model="nuevo.montoInteres" type="number" step="0.01" placeholder="Monto de Interés" class="input" />
          <input v-model="nuevo.calculadoEn" type="date" class="input" placeholder="Fecha de Cálculo" />
          <button type="submit" class="btn col-span-2">{{ editar ? 'Actualizar' : 'Crear' }}</button>
        </form>
      </div>
  
      <div class="card mt-4">
        <ul>
          <li v-for="i in intereses" :key="i.id" class="py-2 border-b flex justify-between items-center">
            <span>ID Tributo: {{ i.idTributo }} - ${{ i.montoInteres }} - {{ i.diasMora }} días</span>
            <div class="space-x-2">
              <button class="btn" @click="seleccionarEditar(i)">Editar</button>
              <button class="btn bg-red-500" @click="eliminarInteres(i.id)">Eliminar</button>
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
      const intereses = ref([]);
      const editar = ref(false);
      const nuevo = ref({
        id: null,
        idTributo: null,
        diasMora: null,
        montoInteres: 0.00,
        calculadoEn: ''
      });
  
      const token = localStorage.getItem('token');
      const headers = { Authorization: `Bearer ${token}` };
  
      const cargarIntereses = async () => {
        const res = await axios.get('/api/admin/gestion/interesmora', { headers });
        intereses.value = res.data;
      };
  
      const crearInteres = async () => {
        if (editar.value) {
          await axios.put(`/api/interesmora/${nuevo.value.id}`, nuevo.value, { headers });
        } else {
          await axios.post('/api/admin/gestion/interesmora', nuevo.value, { headers });
        }
        await cargarIntereses();
        limpiarFormulario();
      };
  
      const eliminarInteres = async (id) => {
        await axios.delete(`/api/admin/gestion/interesmora/${id}`, { headers });
        await cargarIntereses();
      };
  
      const seleccionarEditar = (i) => {
        Object.assign(nuevo.value, { ...i });
        editar.value = true;
      };
  
      const limpiarFormulario = () => {
        Object.assign(nuevo.value, {
          id: null,
          idTributo: null,
          diasMora: null,
          montoInteres: 0.00,
          calculadoEn: ''
        });
        editar.value = false;
      };
  
      onMounted(() => cargarIntereses());
  
      return {
        intereses,
        nuevo,
        editar,
        crearInteres,
        eliminarInteres,
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
  