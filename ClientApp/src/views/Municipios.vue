<template>
  <div class="p-4">
    <div class="mb-4">
      <h2 class="text-2xl font-bold">Municipios</h2>
      <form @submit.prevent="crearMunicipio" class="grid grid-cols-2 gap-4 mt-4">
        <input v-model="nuevo.nombreMunicipio" placeholder="Nombre" class="input" />
        <input v-model="nuevo.provincia" placeholder="Provincia" class="input" />
        <input v-model="nuevo.departamento" placeholder="Departamento" class="input" />
        <input v-model="nuevo.direccion" placeholder="Dirección" class="input" />
        <input v-model="nuevo.telefono" placeholder="Teléfono" class="input" />
        <input v-model="nuevo.email" placeholder="Email" class="input" />
        <input v-model="nuevo.cuit" placeholder="CUIT" class="input" />
        <input v-model="nuevo.responsable" placeholder="Responsable" class="input" />
        <input v-model="nuevo.paginaWeb" placeholder="Página Web" class="input" />
        <button type="submit" class="btn col-span-2">{{ editar ? 'Actualizar' : 'Crear' }}</button>
      </form>
    </div>

    <div class="card mt-4">
      <ul>
        <li v-for="m in municipios" :key="m.id" class="py-2 border-b flex justify-between items-center">
          <span>{{ m.nombreMunicipio }} - {{ m.provincia }}</span>
          <div class="space-x-2">
            <button class="btn" @click="seleccionarEditar(m)">Editar</button>
            <button class="btn bg-red-500" @click="eliminarMunicipio(m.id)">Eliminar</button>
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
    const municipios = ref([]);
    const editar = ref(false);
    const nuevo = ref({
      id: null,
      nombreMunicipio: '',
      provincia: '',
      departamento: '',
      direccion: '',
      telefono: '',
      email: '',
      cuit: '',
      responsable: '',
      paginaWeb: ''
    });

    const token = localStorage.getItem('token');
    const headers = { Authorization: `Bearer ${token}` };

    const cargarMunicipios = async () => {
      const res = await axios.get('/api/admin/catalogos/municipios', { headers });
      municipios.value = res.data;
    };

    const crearMunicipio = async () => {
      if (editar.value) {
        await axios.put(`/api/municipios/${nuevo.value.id}`, nuevo.value, { headers });
      } else {
        await axios.post('/api/admin/catalogos/municipios', nuevo.value, { headers });
      }
      await cargarMunicipios();
      limpiarFormulario();
    };

    const eliminarMunicipio = async (id) => {
      await axios.delete(`/api/admin/catalogos/municipios/${id}`, { headers });
      await cargarMunicipios();
    };

    const seleccionarEditar = (m) => {
      Object.assign(nuevo.value, { ...m });
      editar.value = true;
    };

    const limpiarFormulario = () => {
      Object.assign(nuevo.value, {
        id: null,
        nombreMunicipio: '',
        provincia: '',
        departamento: '',
        direccion: '',
        telefono: '',
        email: '',
        cuit: '',
        responsable: '',
        paginaWeb: ''
      });
      editar.value = false;
    };

    onMounted(() => cargarMunicipios());

    return {
      municipios,
      nuevo,
      editar,
      crearMunicipio,
      eliminarMunicipio,
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
