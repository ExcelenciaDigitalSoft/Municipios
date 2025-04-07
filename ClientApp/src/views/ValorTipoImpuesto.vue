<template>
    <div class="p-4">
      <div class="mb-4">
        <h2 class="text-2xl font-bold">Valores por Tipo de Impuesto</h2>
        <form @submit.prevent="crearValorTipo" class="grid grid-cols-2 gap-4 mt-4">
          <input v-model="nuevo.periodoDesde" type="date" placeholder="Desde" class="input" />
          <input v-model="nuevo.periodoHasta" type="date" placeholder="Hasta" class="input" />
          <input v-model="nuevo.valor" type="number" step="0.01" placeholder="Valor" class="input" />
          <input v-model="nuevo.resolucion" placeholder="Resolución" class="input" />
          <button type="submit" class="btn col-span-2">{{ editar ? 'Actualizar' : 'Crear' }}</button>
        </form>
      </div>
  
      <div class="card mt-4">
        <ul>
          <li v-for="v in valoresTipo" :key="v.id" class="py-2 border-b flex justify-between items-center">
            <span>{{ v.periodoDesde }} a {{ v.periodoHasta }} - ${{ v.valor }}</span>
            <div class="space-x-2">
              <button class="btn" @click="seleccionarEditar(v)">Editar</button>
              <button class="btn bg-red-500" @click="eliminarValorTipo(v.id)">Eliminar</button>
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
      const valoresTipo = ref([]);
      const editar = ref(false);
      const nuevo = ref({
        id: null,
        idTipoImpuesto: 1,
        periodoDesde: '',
        periodoHasta: '',
        valor: 0.00,
        resolucion: ''
      });
  
      const token = localStorage.getItem('token');
      const headers = { Authorization: `Bearer ${token}` };
  
      const cargarValoresTipo = async () => {
        const res = await axios.get('/api/admin/catalogos/valorestipoimpuesto', { headers });
        valoresTipo.value = res.data;
      };
  
      const crearValorTipo = async () => {
        if (editar.value) {
          await axios.put(`/api/valorestipoimpuesto/${nuevo.value.id}`, nuevo.value, { headers });
        } else {
          await axios.post('/api/admin/catalogos/valorestipoimpuesto', nuevo.value, { headers });
        }
        await cargarValoresTipo();
        limpiarFormulario();
      };
  
      const eliminarValorTipo = async (id) => {
        await axios.delete(`/api/admin/catalogos/valorestipoimpuesto/${id}`, { headers });
        await cargarValoresTipo();
      };
  
      const seleccionarEditar = (v) => {
        Object.assign(nuevo.value, { ...v });
        editar.value = true;
      };
  
      const limpiarFormulario = () => {
        Object.assign(nuevo.value, {
          id: null,
          idTipoImpuesto: 1,
          periodoDesde: '',
          periodoHasta: '',
          valor: 0.00,
          resolucion: ''
        });
        editar.value = false;
      };
  
      onMounted(() => cargarValoresTipo());
  
      return {
        valoresTipo,
        nuevo,
        editar,
        crearValorTipo,
        eliminarValorTipo,
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