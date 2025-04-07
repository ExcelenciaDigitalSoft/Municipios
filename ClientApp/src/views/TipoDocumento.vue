<template>
    <div class="p-4">
      <div class="mb-4">
        <h2 class="text-2xl font-bold">Tipos de Documento</h2>
        <form @submit.prevent="crearTipoDocumento" class="grid grid-cols-2 gap-4 mt-4">
          <input v-model="nuevo.descripcion" placeholder="Descripción" class="input" />
          <button type="submit" class="btn col-span-2">{{ editar ? 'Actualizar' : 'Crear' }}</button>
        </form>
      </div>
  
      <div class="card mt-4">
        <ul>
          <li v-for="t in tiposDocumento" :key="t.id" class="py-2 border-b flex justify-between items-center">
            <span>{{ t.descripcion }}</span>
            <div class="space-x-2">
              <button class="btn" @click="seleccionarEditar(t)">Editar</button>
              <button class="btn bg-red-500" @click="eliminarTipoDocumento(t.id)">Eliminar</button>
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
      const tiposDocumento = ref([]);
      const editar = ref(false);
      const nuevo = ref({ id: null, descripcion: '' });
  
      const token = localStorage.getItem('token');
      const headers = { Authorization: `Bearer ${token}` };
  
      const cargarTiposDocumento = async () => {
        const res = await axios.get('/api/admin/catalogos/tipos-documento', { headers });
        tiposDocumento.value = res.data;
      };
  
      const crearTipoDocumento = async () => {
        if (editar.value) {
          await axios.put(`/api/tiposdocumento/${nuevo.value.id}`, nuevo.value, { headers });
        } else {
          await axios.post('/api/admin/catalogos/tipos-documento', nuevo.value, { headers });
        }
        await cargarTiposDocumento();
        limpiarFormulario();
      };
  
      const eliminarTipoDocumento = async (id) => {
        await axios.delete(`/api/admin/catalogos/tipos-documento/${id}`, { headers });
        await cargarTiposDocumento();
      };
  
      const seleccionarEditar = (t) => {
        Object.assign(nuevo.value, { ...t });
        editar.value = true;
      };
  
      const limpiarFormulario = () => {
        Object.assign(nuevo.value, { id: null, descripcion: '' });
        editar.value = false;
      };
  
      onMounted(() => cargarTiposDocumento());
  
      return {
        tiposDocumento,
        nuevo,
        editar,
        crearTipoDocumento,
        eliminarTipoDocumento,
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