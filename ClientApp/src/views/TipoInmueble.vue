<template>
    <div class="p-4">
      <div class="mb-4">
        <h2 class="text-2xl font-bold">Tipos de Inmueble</h2>
        <form @submit.prevent="crearTipoInmueble" class="grid grid-cols-2 gap-4 mt-4">
          <input v-model="nuevo.descripcion" placeholder="Descripción" class="input" />
          <button type="submit" class="btn col-span-2">{{ editar ? 'Actualizar' : 'Crear' }}</button>
        </form>
      </div>
  
      <div class="card mt-4">
        <ul>
          <li v-for="t in tiposInmueble" :key="t.id" class="py-2 border-b flex justify-between items-center">
            <span>{{ t.descripcion }}</span>
            <div class="space-x-2">
              <button class="btn" @click="seleccionarEditar(t)">Editar</button>
              <button class="btn bg-red-500" @click="eliminarTipoInmueble(t.id)">Eliminar</button>
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
      const tiposInmueble = ref([]);
      const editar = ref(false);
      const nuevo = ref({ id: null, descripcion: '' });
  
      const token = localStorage.getItem('token');
      const headers = { Authorization: `Bearer ${token}` };
  
      const cargarTiposInmueble = async () => {
        const res = await axios.get('/api/admin/catalogos/tipos-inmueble', { headers });
        tiposInmueble.value = res.data;
      };
  
      const crearTipoInmueble = async () => {
        if (editar.value) {
          await axios.put(`/api/tiposinmueble/${nuevo.value.id}`, nuevo.value, { headers });
        } else {
          await axios.post('/api/admin/catalogos/tipos-inmueble', nuevo.value, { headers });
        }
        await cargarTiposInmueble();
        limpiarFormulario();
      };
  
      const eliminarTipoInmueble = async (id) => {
        await axios.delete(`/api/admin/catalogos/tipos-inmueble/${id}`, { headers });
        await cargarTiposInmueble();
      };
  
      const seleccionarEditar = (t) => {
        Object.assign(nuevo.value, { ...t });
        editar.value = true;
      };
  
      const limpiarFormulario = () => {
        Object.assign(nuevo.value, { id: null, descripcion: '' });
        editar.value = false;
      };
  
      onMounted(() => cargarTiposInmueble());
  
      return {
        tiposInmueble,
        nuevo,
        editar,
        crearTipoInmueble,
        eliminarTipoInmueble,
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
  