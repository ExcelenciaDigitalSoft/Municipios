<template>
    <div class="p-4">
      <div class="mb-4">
        <h2 class="text-2xl font-bold">Localidades</h2>
        <form @submit.prevent="crearLocalidad" class="grid grid-cols-2 gap-4 mt-4">
          <input v-model="nuevo.departamento" placeholder="Departamento" class="input" />
          <input v-model="nuevo.provincia" placeholder="Provincia" class="input" />
          <input v-model="nuevo.localidad" placeholder="Localidad" class="input" />
          <input v-model="nuevo.codigoPostal" placeholder="Código Postal" class="input" />
          <button type="submit" class="btn col-span-2">{{ editar ? 'Actualizar' : 'Crear' }}</button>
        </form>
      </div>
  
      <div class="card mt-4">
        <ul>
          <li v-for="l in localidades" :key="l.id" class="py-2 border-b flex justify-between items-center">
            <span>{{ l.localidad }} ({{ l.departamento }} - {{ l.provincia }})</span>
            <div class="space-x-2">
              <button class="btn" @click="seleccionarEditar(l)">Editar</button>
              <button class="btn bg-red-500" @click="eliminarLocalidad(l.id)">Eliminar</button>
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
      const localidades = ref([]);
      const editar = ref(false);
      const nuevo = ref({
        id: null,
        departamento: '',
        provincia: '',
        localidad: '',
        codigoPostal: ''
      });
  
      const token = localStorage.getItem('token');
      const headers = { Authorization: `Bearer ${token}` };
  
      const cargarLocalidades = async () => {
        const res = await axios.get('/api/admin/catalogos/localidades', { headers });
        localidades.value = res.data;
      };
  
      const crearLocalidad = async () => {
        if (editar.value) {
          await axios.put(`/api/localidades/${nuevo.value.id}`, nuevo.value, { headers });
        } else {
          await axios.post('/api/admin/catalogos/localidades', nuevo.value, { headers });
        }
        await cargarLocalidades();
        limpiarFormulario();
      };
  
      const eliminarLocalidad = async (id) => {
        await axios.delete(`/api/admin/catalogos/localidades/${id}`, { headers });
        await cargarLocalidades();
      };
  
      const seleccionarEditar = (l) => {
        Object.assign(nuevo.value, { ...l });
        editar.value = true;
      };
  
      const limpiarFormulario = () => {
        Object.assign(nuevo.value, {
          id: null,
          departamento: '',
          provincia: '',
          localidad: '',
          codigoPostal: ''
        });
        editar.value = false;
      };
  
      onMounted(() => cargarLocalidades());
  
      return {
        localidades,
        nuevo,
        editar,
        crearLocalidad,
        eliminarLocalidad,
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
  