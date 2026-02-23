<!-- MENU -->
<TreeView Background="#2E4E4E"
      BorderThickness="0"
      Foreground="White">

    <!-- MATERIAIS -->
    <TreeViewItem Header="📦 Materiais" FontWeight="Bold">
        <TreeViewItem Header="Cadastrar"
                  Selected="CadastrarMaterial"/>
        <TreeViewItem Header="Consultar"
                  Selected="ConsultarMaterial"/>
        <TreeViewItem Header="Excluir"
                  Selected="ExcluirMaterial"/>
    </TreeViewItem>

    <!-- DEPARTAMENTOS -->
    <TreeViewItem Header="🏢 Departamentos" FontWeight="Bold">
        <TreeViewItem Header="Cadastrar"
                  Selected="CadastrarDepartamento"/>
        <TreeViewItem Header="Consultar"
                  Selected="ConsultarDepartamento"/>
    </TreeViewItem>

</TreeView>
