<template>
    <div class="p-4">
      <div class="mb-4">
        <h2 class="text-2xl font-bold">Valuaciones de Inmueble</h2>
        <form @submit.prevent="crearValuacion" class="grid grid-cols-2 gap-4 mt-4">
          <input v-model="nuevo.idInmueble" placeholder="ID Inmueble" class="input" type="number" />
          <input v-model="nuevo.fechaValuacion" type="date" class="input" />
          <input v-model="nuevo.evaluo" type="number" step="0.01" placeholder="Evaluo" class="input" />
          <input v-model="nuevo.fuente" placeholder="Fuente" class="input" />
          <input v-model="nuevo.resolucion" placeholder="Resolución" class="input" />
          <button type="submit" class="btn col-span-2">{{ editar ? 'Actualizar' : 'Crear' }}</button>
        </form>
      </div>
  
      <div class="card mt-4">
        <ul>
          <li v-for="v in valuaciones" :key="v.id" class="py-2 border-b flex justify-between items-center">
            <span>ID Inmueble: {{ v.idInmueble }} - ${{ v.evaluo }} - {{ v.fechaValuacion }}</span>
            <div class="space-x-2">
              <button class="btn" @click="seleccionarEditar(v)">Editar</button>
              <button class="btn bg-red-500" @click="eliminarValuacion(v.id)">Eliminar</button>
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
      const valuaciones = ref([]);
      const editar = ref(false);
      const nuevo = ref({
        id: null,
        idInmueble: null,
        fechaValuacion: '',
        evaluo: 0,
        fuente: '',
        resolucion: ''
      });
  
      const token = localStorage.getItem('token');
      const headers = { Authorization: `Bearer ${token}` };
  
      const cargarValuaciones = async () => {
        const res = await axios.get('/api/admin/gestion/valuacionesinmueble', { headers });
        valuaciones.value = res.data;
      };
  
      const crearValuacion = async () => {
        if (editar.value) {
          await axios.put(`/api/valuacionesinmueble/${nuevo.value.id}`, nuevo.value, { headers });
        } else {
          await axios.post('/api/admin/gestion/valuacionesinmueble', nuevo.value, { headers });
        }
        await cargarValuaciones();
        limpiarFormulario();
      };
  
      const eliminarValuacion = async (id) => {
        await axios.delete(`/api/admin/gestion/valuacionesinmueble/${id}`, { headers });
        await cargarValuaciones();
      };
  
      const seleccionarEditar = (v) => {
        Object.assign(nuevo.value, { ...v });
        editar.value = true;
      };
  
      const limpiarFormulario = () => {
        Object.assign(nuevo.value, {
          id: null,
          idInmueble: null,
          fechaValuacion: '',
          evaluo: 0,
          fuente: '',
          resolucion: ''
        });
        editar.value = false;
      };
  
      onMounted(() => cargarValuaciones());
  
      return {
        valuaciones,
        nuevo,
        editar,
        crearValuacion,
        eliminarValuacion,
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
  