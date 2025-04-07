<template>
    <div class="p-4">
      <div class="mb-4">
        <h2 class="text-2xl font-bold">Inmuebles en Obras Públicas</h2>
        <form @submit.prevent="crearRelacion" class="grid grid-cols-2 gap-4 mt-4">
          <input v-model="nuevo.idInmueble" placeholder="ID Inmueble" class="input" type="number" />
          <input v-model="nuevo.idObra" placeholder="ID Obra Pública" class="input" type="number" />
          <input v-model="nuevo.porcentajeParticipacion" placeholder="% Participación" class="input" type="number" step="0.01" />
          <button type="submit" class="btn col-span-2">{{ editar ? 'Actualizar' : 'Agregar' }}</button>
        </form>
      </div>
  
      <div class="card mt-4">
        <ul>
          <li v-for="i in inmuebleObra" :key="i.id" class="py-2 border-b flex justify-between items-center">
            <span>Inmueble: {{ i.idInmueble }}, Obra: {{ i.idObra }}, %: {{ i.porcentajeParticipacion }}</span>
            <div class="space-x-2">
              <button class="btn" @click="seleccionarEditar(i)">Editar</button>
              <button class="btn bg-red-500" @click="eliminarRelacion(i.idInmueble, i.idObra)">Eliminar</button>
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
      const inmuebleObra = ref([]);
      const editar = ref(false);
      const nuevo = ref({
        idInmueble: null,
        idObra: null,
        porcentajeParticipacion: 0
      });
  
      const token = localStorage.getItem('token');
      const headers = { Authorization: `Bearer ${token}` };
  
      const cargarRelaciones = async () => {
        const res = await axios.get('/api/admin/gestion/inmueblesobras', { headers });
        inmuebleObra.value = res.data;
      };
  
      const crearRelacion = async () => {
        const ruta = `/api/admin/gestion/inmueblesobras/${nuevo.value.idInmueble}/${nuevo.value.idObra}`;
        if (editar.value) {
          await axios.put(ruta, nuevo.value, { headers });
        } else {
          await axios.post('/api/admin/gestion/inmueblesobras', nuevo.value, { headers });
        }
        await cargarRelaciones();
        limpiarFormulario();
      };
  
      const eliminarRelacion = async (idInmueble, idObra) => {
        await axios.delete(`/api/admin/gestion/inmueblesobras/${idInmueble}/${idObra}`, { headers });
        await cargarRelaciones();
      };
  
      const seleccionarEditar = (i) => {
        Object.assign(nuevo.value, { ...i });
        editar.value = true;
      };
  
      const limpiarFormulario = () => {
        Object.assign(nuevo.value, {
          idInmueble: null,
          idObra: null,
          porcentajeParticipacion: 0
        });
        editar.value = false;
      };
  
      onMounted(() => cargarRelaciones());
  
      return {
        inmuebleObra,
        nuevo,
        editar,
        crearRelacion,
        eliminarRelacion,
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
  