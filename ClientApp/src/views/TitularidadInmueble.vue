<template>
    <div class="p-4">
      <div class="mb-4">
        <h2 class="text-2xl font-bold">Titularidad de Inmuebles</h2>
        <form @submit.prevent="crearTitularidad" class="grid grid-cols-2 gap-4 mt-4">
          <input v-model="nuevo.idInmueble" placeholder="ID Inmueble" class="input" type="number" />
          <input v-model="nuevo.idContribuyente" placeholder="ID Contribuyente" class="input" type="number" />
          <input v-model="nuevo.fechaInicio" type="date" placeholder="Fecha Inicio" class="input" />
          <input v-model="nuevo.fechaFin" type="date" placeholder="Fecha Fin" class="input" />
          <button type="submit" class="btn col-span-2">{{ editar ? 'Actualizar' : 'Crear' }}</button>
        </form>
      </div>
  
      <div class="card mt-4">
        <ul>
          <li v-for="t in titularidades" :key="t.id" class="py-2 border-b flex justify-between items-center">
            <span>ID Inmueble: {{ t.idInmueble }} - ID Contribuyente: {{ t.idContribuyente }}</span>
            <div class="space-x-2">
              <button class="btn" @click="seleccionarEditar(t)">Editar</button>
              <button class="btn bg-red-500" @click="eliminarTitularidad(t.id)">Eliminar</button>
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
      const titularidades = ref([]);
      const editar = ref(false);
      const nuevo = ref({
        id: null,
        idInmueble: null,
        idContribuyente: null,
        fechaInicio: '',
        fechaFin: ''
      });
  
      const token = localStorage.getItem('token');
      const headers = { Authorization: `Bearer ${token}` };
  
      const cargarTitularidades = async () => {
        const res = await axios.get('/api/admin/gestion/titularidadinmueble', { headers });
        titularidades.value = res.data;
      };
  
      const crearTitularidad = async () => {
        if (editar.value) {
          await axios.put(`/api/titularidadinmueble/${nuevo.value.id}`, nuevo.value, { headers });
        } else {
          await axios.post('/api/admin/gestion/titularidadinmueble', nuevo.value, { headers });
        }
        await cargarTitularidades();
        limpiarFormulario();
      };
  
      const eliminarTitularidad = async (id) => {
        await axios.delete(`/api/admin/gestion/titularidadinmueble/${id}`, { headers });
        await cargarTitularidades();
      };
  
      const seleccionarEditar = (t) => {
        Object.assign(nuevo.value, { ...t });
        editar.value = true;
      };
  
      const limpiarFormulario = () => {
        Object.assign(nuevo.value, {
          id: null,
          idInmueble: null,
          idContribuyente: null,
          fechaInicio: '',
          fechaFin: ''
        });
        editar.value = false;
      };
  
      onMounted(() => cargarTitularidades());
  
      return {
        titularidades,
        nuevo,
        editar,
        crearTitularidad,
        eliminarTitularidad,
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
  