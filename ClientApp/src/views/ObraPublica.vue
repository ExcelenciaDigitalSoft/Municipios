<template>
    <div class="p-4">
      <div class="mb-4">
        <h2 class="text-2xl font-bold">Obras Públicas</h2>
        <form @submit.prevent="crearObra" class="grid grid-cols-2 gap-4 mt-4">
          <input v-model="nuevo.descripcion" placeholder="Descripción" class="input" />
          <input v-model="nuevo.fechaInicio" type="date" class="input" />
          <input v-model="nuevo.fechaFin" type="date" class="input" />
          <input v-model="nuevo.costoTotal" type="number" step="0.01" placeholder="Costo Total" class="input" />
          <button type="submit" class="btn col-span-2">{{ editar ? 'Actualizar' : 'Crear' }}</button>
        </form>
      </div>
  
      <div class="card mt-4">
        <ul>
          <li v-for="o in obras" :key="o.id" class="py-2 border-b flex justify-between items-center">
            <span>{{ o.descripcion }} - ${{ o.costoTotal }} ({{ o.fechaInicio }})</span>
            <div class="space-x-2">
              <button class="btn" @click="seleccionarEditar(o)">Editar</button>
              <button class="btn bg-red-500" @click="eliminarObra(o.id)">Eliminar</button>
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
      const obras = ref([]);
      const editar = ref(false);
      const nuevo = ref({
        id: null,
        descripcion: '',
        fechaInicio: '',
        fechaFin: '',
        costoTotal: 0,
        idMunicipio: 1,
        idEstado: 1
      });
  
      const token = localStorage.getItem('token');
      const headers = { Authorization: `Bearer ${token}` };
  
      const cargarObras = async () => {
        const res = await axios.get('/api/admin/gestion/obraspublicas', { headers });
        obras.value = res.data;
      };
  
      const crearObra = async () => {
        if (editar.value) {
          await axios.put(`/api/obraspublicas/${nuevo.value.id}`, nuevo.value, { headers });
        } else {
          await axios.post('/api/admin/gestion/obraspublicas', nuevo.value, { headers });
        }
        await cargarObras();
        limpiarFormulario();
      };
  
      const eliminarObra = async (id) => {
        await axios.delete(`/api/admin/gestion/obraspublicas/${id}`, { headers });
        await cargarObras();
      };
  
      const seleccionarEditar = (o) => {
        Object.assign(nuevo.value, { ...o });
        editar.value = true;
      };
  
      const limpiarFormulario = () => {
        Object.assign(nuevo.value, {
          id: null,
          descripcion: '',
          fechaInicio: '',
          fechaFin: '',
          costoTotal: 0,
          idMunicipio: 1,
          idEstado: 1
        });
        editar.value = false;
      };
  
      onMounted(() => cargarObras());
  
      return {
        obras,
        nuevo,
        editar,
        crearObra,
        eliminarObra,
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
  